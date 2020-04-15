using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, ITakeDamage<int>, IHavePiksels
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    public int MaxHealth { get { return maxHealth; } }
    public int Health { get { return health; } }

    [Header("Attack")]
    [SerializeField] private float attackRadius = 25f;
    [SerializeField] private float attacksPerSecond = 0.5f;
    private float nextTimeToAttack = 0f;
    private Transform target;
    public GameObject fireball;
    [SerializeField] private float fireballSpeed = 20f;

    [SerializeField] private float lookRadius = 60f;
    private NavMeshAgent agent;

    [Header("Other")]
    public GameObject deathEffectGO;
    public HealthBar healthBar;

    private SoundManager soundManager;

    [Header("Drop")]
    [SerializeField] private int minPixelsDrop = 5;
    [SerializeField] private int maxPixelsDrop = 15;
    public int MinPixelsDrop { get { return minPixelsDrop; } }
    public int MaxPixelsDrop { get { return maxPixelsDrop; } }

    void Start()
    {
        health = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRadius;
        healthBar.SetMaxHealth(maxHealth);
        soundManager = SoundManager.instance;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            if (distance <= attackRadius)
            {
                FaceTarget();

                if (Time.time >= nextTimeToAttack && Time.timeScale > 0f)
                {
                    nextTimeToAttack = Time.time + 1f / attacksPerSecond;
                    AttackTarget();
                }
            }
            agent.SetDestination(target.position);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
        else
            soundManager.PlaySound("EnemyTakeDamage");

        healthBar.SetHealth(health);
    }

    public void Die()
    {
        soundManager.PlaySound("EnemyDie");
        GameObject deathEffect = Instantiate(deathEffectGO, gameObject.transform.position + new Vector3(0f, .5f, 0f), Quaternion.LookRotation(new Vector3(0f, 0f, 0f)));
        VisualEffect death = deathEffect.GetComponent<VisualEffect>();

        MeshRenderer myRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        if (myRenderer != null)
        {
            Gradient gradient = MaterialsColorManager.instance.NiceGradientForEnemyDeathEffect(myRenderer.material);
            death.SetGradient("Color", gradient);
        }

        death.Play();
        Destroy(deathEffect, 15f);
        gameObject.transform.position = new Vector3(0f, 0f, 0f);
        Destroy(gameObject, .1f);
        DropPixels();
    }

    public int DropPixels()
    {
        int Pixels = Random.Range(MinPixelsDrop, MaxPixelsDrop);
        PlayerManager.instance.player.GetComponent<Player>().Pixels += Pixels;
        return Pixels;
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void AttackTarget()
    {
        GameObject fireBall = Instantiate(fireball, transform.position, transform.rotation);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();
        
        if(rb != null)
            rb.velocity = (target.transform.position - transform.position).normalized * fireballSpeed;
        
        Destroy(fireBall, 5f);
    }
}
