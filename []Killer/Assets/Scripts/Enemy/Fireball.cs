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
        if (!other.CompareTag("Enemy") && !other.CompareTag("Ignore"))
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                    player.TakeDamage(damage);
            }

            Debug.Log(other.tag);
            Debug.Log(other.gameObject.name);

            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            explosion.GetComponent<VisualEffect>().Play();
            Destroy(explosion, 3f);
            Destroy(gameObject);
        }
    }

}
