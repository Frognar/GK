using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fireball : MonoBehaviour
{

    public GameObject explosionVFX;

    [SerializeField] private int m_damage = 35;
    public int damage { get { return m_damage; } set { m_damage = value; } }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" && other.tag != "Bullet")
        {
            if (other.tag == "Player")
                Player.instance.TakeDamage(damage);

            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            explosion.GetComponent<VisualEffect>().Play();
            Destroy(explosion, 3f);
            Destroy(gameObject);
        }
    }

}
