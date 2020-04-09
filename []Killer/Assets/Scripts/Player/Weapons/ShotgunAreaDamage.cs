using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAreaDamage : MonoBehaviour
{

    [SerializeField] private int m_damage = 15;
    public int damage { get { return m_damage; } set { m_damage = value; } }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
