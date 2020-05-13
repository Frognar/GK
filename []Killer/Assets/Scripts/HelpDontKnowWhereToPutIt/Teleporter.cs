using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author:          Anna Mach
 * Date:            12.05.2020
 * Collaborators:   
 */
public class Teleporter : MonoBehaviour
{
    public GameObject placeToTeleport;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = placeToTeleport.transform.position;
        }
    }
}
