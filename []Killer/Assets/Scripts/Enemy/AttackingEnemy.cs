using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingEnemy : Enemy
{
    [Header("Attack")]
    [SerializeField] private float attackRadius = 25f;
    [SerializeField] private float attacksPerSecond = 0.5f;
    private float nextTimeToAttack = 0f;
    private Transform target;
    public GameObject fireball;
    [SerializeField] private float fireballSpeed = 20f;

    [SerializeField] private float lookRadius = 60f;

    private AttackingMoveAround attackingMoveAround;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        soundManager = SoundManager.instance;
        target = PlayerManager.instance.player.transform;
        attackingMoveAround = GetComponent<AttackingMoveAround>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;//Vector3.Distance(target.position, transform.position);
        attackingMoveAround.SetTargerPosition(Vector3.zero);

        if (distance <= lookRadius)
        {
            FaceTarget();
            if (distance <= attackRadius)
            {
                if (Time.time >= nextTimeToAttack && Time.timeScale > 0f)
                {
                    nextTimeToAttack = Time.time + 1f / attacksPerSecond;
                    AttackTarget();
                }
            }
            else
                attackingMoveAround.SetTargerPosition(target.position);
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

        Destroy(fireBall, 15f);
    }
}
