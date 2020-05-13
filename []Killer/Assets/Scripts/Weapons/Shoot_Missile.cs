using System.Collections.Generic;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class Shoot_Missile : MonoBehaviour, IShoot {
    private float fireRate = 3f;
    [SerializeField] private LayerMask targetList;
    private Transform raycastFrom;
    public float FireRate {
        get {
            return 1f / fireRate;
        }
    }
    private int pixelCost = 5;
    public int PixelCost {
        get {
            return pixelCost;
        }
    }

    public GameObject missile;
    [SerializeField] private float missileSpeed = 20f;

    private void Start () {
        raycastFrom = GetComponentInParent<Camera> ().transform;
        if (raycastFrom == null)
            Debug.LogWarning ("No raycastFrom in gun!");
    }

    public void Shoot () {
        GameObject missleGO = Instantiate (missile, raycastFrom.position + raycastFrom.forward, raycastFrom.rotation);
        
        Fireball fireball = missleGO.GetComponent<Fireball> ();
        if (fireball != null)
            fireball.SetTargetList (targetList);

        Rigidbody rb = missleGO.GetComponent<Rigidbody> ();
        if (rb != null)
            rb.velocity = raycastFrom.forward * missileSpeed;

        Destroy (missleGO, 15f);
    }
}
