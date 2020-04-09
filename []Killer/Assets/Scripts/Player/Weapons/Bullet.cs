using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int m_damage = 0;
    private int m_areaDamage = 0;
    private float m_areaRange = 0.5f;

    public int damage { get { return m_damage; } set { m_damage = value; } }
    public int areaDamage { get { return m_areaDamage; } set { m_areaDamage = value; } }
    public float areaRange { get { return m_areaRange; } set { m_areaRange = value; } }

    private SphereCollider collider;
    private Rigidbody rb;

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(rb.velocity.magnitude);
    }

    public void SetRadius()
    {
        collider.radius = areaRange;
    }

    void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Enemy")
        //{
        Enemy target = other.gameObject.GetComponent<Enemy>();
        if (target != null)
        {
            Debug.Log("Mam! " + other.tag);
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance < areaRange / 2f)
                target.TakeDamage(damage);
            else
                target.TakeDamage(areaDamage);
        }
        //}
    }

}
