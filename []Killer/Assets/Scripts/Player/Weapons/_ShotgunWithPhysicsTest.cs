using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class _ShotgunWithPhysicsTest : MonoBehaviour, IShotable<int>
{
    #region Zmienne
    #region IShotable
    [SerializeField] private int m_damage = 50;
    [SerializeField] private int m_areaDamage = 10;
    [SerializeField] private float m_areaRange = 10f;
    [SerializeField] private float m_range = 90f;
    [SerializeField] private float m_fireRate = 1f;
    [SerializeField] private float m_impactForce = 260f;
    public int damage { get { return m_damage; } set { m_damage = value; } }
    public int areaDamage { get { return m_areaDamage; } set { m_areaDamage = value; } }
    public float areaRange { get { return m_areaRange; } set { m_areaRange = value; } }
    public float range { get { return m_range; } set { m_range = value; } }
    public float fireRate { get { return m_fireRate; } set { m_fireRate = value; } }
    public float impactForce { get { return m_impactForce; } set { m_impactForce = value; } }
    #endregion

    private float nextTimeToFire = 0f;
    public Camera fpsCam;
    public VisualEffect shotEffect;
    public GameObject impactEffect;

    public GameObject bulletGO;
    public Transform barrel;
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
        GameObject bulletIns = Instantiate(bulletGO, barrel.position, barrel.rotation);
        Bullet bullet = bulletIns.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.damage = damage;
            bullet.areaDamage = areaDamage;
            bullet.areaRange = areaRange;
            bullet.SetRadius();
        }
        Rigidbody rb = bulletIns.GetComponent<Rigidbody>();
        if (rb != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
                rb.velocity = (hit.point - barrel.position).normalized * range;
            else
                rb.velocity = barrel.forward * range;
        }
        Destroy(bulletIns, 1f);
    }

}
