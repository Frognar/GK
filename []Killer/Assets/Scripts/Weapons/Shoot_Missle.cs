using System.Collections.Generic;
using UnityEngine;

public class Shoot_Missle : MonoBehaviour, IShoot {
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

    public GameObject missle;
    [SerializeField] private float missleSpeed = 20f;

    private void Start () {
        raycastFrom = GetComponentInParent<Camera> ().transform;
        if (raycastFrom == null)
            Debug.LogWarning ("No raycastFrom in gun!");
    }

    public void Shoot () {
        GameObject missleGO = Instantiate (missle, raycastFrom.position + raycastFrom.forward, raycastFrom.rotation);
        
        Fireball fireball = missleGO.GetComponent<Fireball> ();
        if (fireball != null)
            fireball.SetTargetList (targetList);

        Rigidbody rb = missleGO.GetComponent<Rigidbody> ();
        if (rb != null)
            rb.velocity = raycastFrom.forward * missleSpeed;

        Destroy (missleGO, 15f);
    }
}
