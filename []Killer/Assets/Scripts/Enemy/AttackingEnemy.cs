using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class AttackingEnemy : Enemy
{
    [Header("Attack")]
    [SerializeField] private float attackRadius = 25f;
    [SerializeField] private float attacksPerSecond = 0.5f;
    private float nextTimeToAttack = 0f;
    private Transform target;
    public GameObject fireball;
    [SerializeField] private float fireballSpeed = 20f;

    private NavMeshAgent agent;
    [SerializeField] private float lookRadius = 60f;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundManager.instance;
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        agent.stoppingDistance = attackRadius;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
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

        if (rb != null)
            rb.velocity = (target.transform.position - transform.position).normalized * fireballSpeed;

        Destroy(fireBall, 5f);
    }
}
