using UnityEngine.VFX;
using UnityEngine;

[RequireComponent(typeof(VisualEffect))]
public class Gun : MonoBehaviour
{
    [SerializeField] private GunPattern gunPattern;

    private int damage = 35;
    private float range = 150f;
    private float fireRate = 3f;
    private float impactForce = 60f;
    private int bulletsCount = 1;
    private float shotInacurracy = .5f;
    private float nextTimeToFire = 0f;
    private Transform raycastFrom;

    private GameObject impactEffectPrefab;
    private VisualEffect shotEffect;
    private Animator animator;

    private string shotSoundName = "RifleShot";
    private SoundManager soundManager;

    // Set up weapon stats from ScriptableObject
    private void SetUpGun()
    {
        damage = gunPattern.damage;
        range = gunPattern.range;
        fireRate = gunPattern.fireRate;
        impactForce = gunPattern.impactForce;
        bulletsCount = gunPattern.bulletsCount;
        shotInacurracy = gunPattern.shotInacurracy;
        shotSoundName = gunPattern.shotSoundName;
        impactEffectPrefab = gunPattern.impactEffectPrefab;
    }

    private void Start()
    {
        soundManager = SoundManager.instance;

        SetUpGun();
        shotEffect = GetComponent<VisualEffect>();

        raycastFrom = PlayerManager.instance.player.GetComponentInChildren<Camera>().transform;
        if (raycastFrom == null)
            Debug.LogError("No raycastFrom in gun!");

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("No animator in gun!");
    }

    private void Update()
    {
        if (!PauseManager.gameIsPaused && PlayerManager.instance.player.GetComponent<Player>().IsAlive)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shot();
            }
        }
    }

    private void ShotEffectPlay()
    {
        shotEffect.SendEvent("OnPlay");
        soundManager.PlaySound(shotSoundName);
    }

    private void Shot()
    {
        if (PlayerManager.instance.player.GetComponent<Player>().UsePixels(bulletsCount))
        {
            animator.SetTrigger("Shot");
            for (int i = 0; i < bulletsCount; i++)
            {
                Vector3 shotInacurracyVector = new Vector3(Random.Range(-shotInacurracy, shotInacurracy), Random.Range(-shotInacurracy, shotInacurracy), Random.Range(-shotInacurracy, shotInacurracy));
                Vector3 direction = raycastFrom.forward * range + shotInacurracyVector;

                if (Physics.Raycast(raycastFrom.position, direction, out RaycastHit hitInfo, range))
                {
                    if (hitInfo.rigidbody != null)
                        hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce / bulletsCount);

                    SpawnImpactEffect(hitInfo);

                    ITakeDamage<int> hittedObject = hitInfo.transform.GetComponent<ITakeDamage<int>>();
                    if (hittedObject != null)
                        hittedObject.TakeDamage(damage / bulletsCount);

                }
            }
        }        
    }

    private void SpawnImpactEffect(RaycastHit hitInfo)
    {
        GameObject ImpactEffectGO = Instantiate(impactEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        VisualEffect VFX = ImpactEffectGO.GetComponent<VisualEffect>();
        if (VFX != null)
        {
            MeshRenderer targetMesh = hitInfo.transform.gameObject.GetComponentInChildren<MeshRenderer>();
            if (targetMesh != null)
            {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForImpactEffect(targetMesh.material);
                if (gradient != null)
                    VFX.SetGradient("Color", gradient);
            }
            VFX.Play();
        }

        Destroy(ImpactEffectGO, 1f);
    }
}
