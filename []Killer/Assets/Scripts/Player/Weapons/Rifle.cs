using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Rifle : MonoBehaviour, IShotable<int>
{
    // -----------From IShotable-----------
    public int damage { get; set; }
    public float range { get; set; }
    public float fireRate { get; set; }
    public float impactForce { get; set; }

    public void Shot()
    {
        shotEffect.SendEvent("OnPlay");
        FindObjectOfType<AudioManager>().Play("RifleShot");

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
                enemy.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
    // ------------------------------------

    private float nextTimeToFire { get; set; }
    public Camera fpsCam;
    public VisualEffect shotEffect;
    public GameObject impactEffect;

    Rifle()
    {
        damage = 10;
        range = 100f;
        fireRate = 5f;
        impactForce = 45f;
        nextTimeToFire = 0f;
    }

    public void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Time.timeScale > 0f && PlayerManager.instance.player.GetComponent<Player>().isAlive)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shot();
        }

    }
}