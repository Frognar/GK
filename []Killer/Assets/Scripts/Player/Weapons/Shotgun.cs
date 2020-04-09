using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : MonoBehaviour, IShotable<int>
{
    #region Zmienne
    #region IShotable
    [SerializeField] private int m_damage = 50;
    [SerializeField] private float m_range = 20f;
    [SerializeField] private float m_fireRate = 1f;
    [SerializeField] private float m_impactForce = 260f;
    public int damage { get { return m_damage; } set { m_damage = value; } }
    public float range { get { return m_range; } set { m_range = value; } }
    public float fireRate { get { return m_fireRate; } set { m_fireRate = value; } }
    public float impactForce { get { return m_impactForce; } set { m_impactForce = value; } }
    #endregion

    private float nextTimeToFire = 0f;
    public Camera fpsCam;
    public VisualEffect shotEffect;
    public GameObject impactEffect;
    #endregion

    void Update()
    {

        if (!GameManager.instance.gameIsPaused && Player.instance.isAlive)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shot();
            }
        }

    }

    public void Shot()
    {
        shotEffect.SendEvent("OnPlay");
        AudioManager.instance.Play("ShotgunShot");

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
            MeshRenderer targetMesh = hit.transform.gameObject.GetComponentInChildren<MeshRenderer>();
            if (targetMesh != null)
            {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForImpactEffect(targetMesh.material);
                if (gradient != null)
                    impactGO.GetComponent<VisualEffect>().SetGradient("Color", gradient);
            }

            Destroy(impactGO, 2f);
        }
    }

}
