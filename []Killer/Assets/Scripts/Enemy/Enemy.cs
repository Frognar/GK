using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int damage = 10;

    public float lookRadius = 15f;
    public float attackRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
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
                AttackTarget();
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
        ;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
        else
            FindObjectOfType<AudioManager>().Play("EnemyTakeDamage");

        healthBar.SetHealth(currentHealth);
    }

    void Death()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
