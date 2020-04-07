using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IKillable<int>
{
    // -----------IKillable-----------
    public int health { get; set; }
    public int maxHealth { get; set; }

    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0)
            Die();
        else
            FindObjectOfType<AudioManager>().Play("EnemyTakeDamage");

        healthBar.SetHealth(health);
    }
    
    public void Die() {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Gradient gradient;
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        gradient = new Gradient();
        colorKey = new GradientColorKey[1];
        alphaKey = new GradientAlphaKey[2];
        colorKey[0].color = gameObject.GetComponentInChildren<MeshRenderer>().material.GetColor("_BaseColor");
        Debug.Log(colorKey[0].color);
        colorKey[0].time = 0f;
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0f;
        gradient.SetKeys(colorKey, alphaKey);

        GameObject deathEffect = Instantiate(enemyDeath, gameObject.transform.position + new Vector3(0f, .5f, 0f), Quaternion.LookRotation(new Vector3(0f, 0f, 0f)));
        VisualEffect death = deathEffect.GetComponent<VisualEffect>();
        death.SetGradient("Color", gradient);
        death.Play();
        
        Destroy(deathEffect, 5f);
        Destroy(gameObject, .1f);
    }
    // -------------------------------

    public int damage = 10;

    public float lookRadius = 15f;
    public float attackRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    public GameObject enemyDeath;

    public HealthBar healthBar;
    public GameObject fireball;
    public float fireballSpeed = 20f;
    public float fireballFireRate = .5f;
    private float nextTimeToFire = 0f;
    public Transform fbSpawner;

    Enemy()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRadius;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            if (distance <= attackRadius)
            {
                FaceTarget();

                if (Time.time >= nextTimeToFire && Time.timeScale > 0f)
                {
                    nextTimeToFire = Time.time + 1f / fireballFireRate;
                    AttackTarget();
                }
            }
            agent.SetDestination(target.position);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackTarget()
    {
        GameObject fireBall = Instantiate(fireball, fbSpawner.position, transform.rotation);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * fireballSpeed;
        Destroy(fireBall, 5f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
