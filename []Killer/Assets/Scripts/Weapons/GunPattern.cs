using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Guns/Gun", order = 1)]
public class GunPattern : ScriptableObject
{
    public int damage = 35;
    public float range = 150f;
    public float fireRate = 3f;
    public float impactForce = 60f;
    public int bulletsCount = 1;
    public float shotInacurracy = .5f;
    public string shotSoundName = "RifleShot";
    public GameObject impactEffectPrefab;
    public VisualEffect shotEffect;
}
