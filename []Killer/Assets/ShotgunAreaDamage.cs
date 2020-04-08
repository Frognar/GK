using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAreaDamage : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(10);
        }
    }

}
