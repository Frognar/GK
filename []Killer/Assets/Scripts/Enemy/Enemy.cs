using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IKillable<int>, IAttackableAI, IWalkableAI
{
    #region Zmianne
    #region IKillable
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_health;
    public int maxHealth { get { return m_maxHealth; } set { m_maxHealth = value; } }
    public int health { get { return m_health; } set { m_health = value; } }
    #endregion

    #region IAttackableAI
    [SerializeField] private float m_attackRadius;
    [SerializeField] private float m_attacksPerSecond;
    private float m_nextTimeToAttack;
    private Player m_target;
    public float attackRadius { get { return m_attackRadius; } set { m_attackRadius = value; } }
    public float attacksPerSecond { get { return m_attacksPerSecond; } set { m_attacksPerSecond = value; } }
    public float nextTimeToAttack { get { return m_nextTimeToAttack; } set { m_nextTimeToAttack = value; } }
    public Player target { get { return m_target; } set { m_target = value; } }
    #endregion

    #region IWalkableAI
    [SerializeField] private float m_lookRadius;
    private NavMeshAgent m_agent;
    public float lookRadius { get { return m_lookRadius; } set { m_lookRadius = value; } }
    public NavMeshAgent agent { get { return m_agent; } set { m_agent = value; } }
    #endregion

    public GameObject deathEffectGO;
    public HealthBar healthBar;
    public GameObject fireball;
    [SerializeField] private float m_fireballSpeed = 20f;
    public float fireballSpeed { get { return m_fireballSpeed; } set { m_fireballSpeed = value; } }
    #endregion

    Enemy()
    {
        maxHealth = 100;
        attackRadius = 25f;
        attacksPerSecond = 0.5f;
        fireballSpeed = 20f;
        lookRadius = 60f;
        nextTimeToAttack = 0f;
    }

    void Start()
    {
        health = maxHealth;
        target = Player.instance;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRadius;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        float distance = DistanceToTarget(target.transform);

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
            GoToTarget(target.transform);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    #region IKillable
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
        else
            AudioManager.instance.Play("EnemyTakeDamage");

        healthBar.SetHealth(health);
    }

    public void Die()
    {
        AudioManager.instance.Play("EnemyDeath");
        GameObject deathEffect = Instantiate(deathEffectGO, gameObject.transform.position + new Vector3(0f, .5f, 0f), Quaternion.LookRotation(new Vector3(0f, 0f, 0f)));
        VisualEffect death = deathEffect.GetComponent<VisualEffect>();

        MeshRenderer myRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        if (myRenderer != null)
        {
            Gradient gradient = MaterialsColorManager.instance.NiceGradientForEnemyDeathEffect(myRenderer.material);
            death.SetGradient("Color", gradient);
        }

        death.Play();
        Destroy(deathEffect, 5f);
        Destroy(gameObject, .1f);
    }
    #endregion

    #region IAttackableAI
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
    #endregion

    #region IWalkableAI
    public float DistanceToTarget(Transform target)
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public void GoToTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }
    #endregion

}
