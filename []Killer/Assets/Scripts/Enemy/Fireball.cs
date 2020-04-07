using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fireball : MonoBehaviour
{

    public GameObject explosionVFX;



    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.instance.player.GetComponent<Player>().TakeDamage(50);
            Debug.Log("Player");
        }
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        explosion.GetComponent<VisualEffect>().Play();
        Destroy(explosion, 3f);
        Destroy(gameObject);
    }

}
