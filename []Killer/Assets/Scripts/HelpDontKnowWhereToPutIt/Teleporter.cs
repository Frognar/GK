using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Anna Mach 12.05.2020
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
