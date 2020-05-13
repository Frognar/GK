using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
[CreateAssetMenu (fileName = "NewGun", menuName = "Gun")]
public class GunPattern : ScriptableObject
{
    public enum ShotSFXName {
        RifleShot,
        PistolShot,
        ShotgunShot
    }

    [SerializeField] private int damage = 35;
    public int Damage => damage;
    [SerializeField] private float range = 150f;
    public float Range => range;

    [SerializeField] private float fireRate = 3f;
    public float FireRate => fireRate;

    [SerializeField] private float impactForce = 60f;
    public float ImpactForce => impactForce;

    [SerializeField] private int bulletsCount = 1;
    public int BulletsCount => bulletsCount;

    [SerializeField] private float shotInacurracy = .5f;
    public float ShotInacurracy => shotInacurracy;

    [SerializeField] private ShotSFXName shotSoundName = ShotSFXName.RifleShot;
    public ShotSFXName ShotSoundName => shotSoundName;

    [SerializeField] private GameObject impactEffectPrefab;
    public GameObject ImpactEffectPrefab => impactEffectPrefab;

    [SerializeField] private int pixelCost = 1;
    public int PixelCost => pixelCost;
}
