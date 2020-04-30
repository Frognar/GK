using UnityEngine.VFX;
using UnityEngine;

public class Shoot_Gun: MonoBehaviour, IShoot
{
    [SerializeField] private GunPattern gunPattern;

    private int damage = 35;
    private float range = 150f;
    private float fireRate = 3f;
    public float FireRate {
        get {
            return 1f / fireRate;
        }
    }
    private float impactForce = 60f;
    private int bulletsCount = 1;
    public int PixelCost {
        get {
            return bulletsCount;
        }
    }
    private float shotInacurracy = .5f;
    private Transform raycastFrom;

    private GameObject impactEffectPrefab;
    private VisualEffect shotEffect;
    private Animator animator;

    private string shotSoundName = "RifleShot";
    private SoundPlayer soundPlayer;

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

    private void Start () {
        SetUpGun ();

        soundPlayer = GetComponent<SoundPlayer> ();
        if (soundPlayer == null)
            Debug.LogWarning ("No soundPlayer in gun!");

        shotEffect = GetComponent<VisualEffect> ();
        if (shotEffect == null)
            Debug.LogWarning ("No shotEffect in gun!");

        raycastFrom = GetComponentInParent<Camera> ().transform;
        if (raycastFrom == null)
            Debug.LogWarning ("No raycastFrom in gun!");

        animator = GetComponent<Animator> ();
        if (animator == null)
            Debug.LogWarning ("No animator in gun!");
    }

    private void ShotEffectPlay () {
        animator?.SetTrigger ("Shot");
        shotEffect?.SendEvent ("OnPlay");
        soundPlayer?.PlaySoundEvent(shotSoundName);
    }

    public void Shoot () {
        ShotEffectPlay ();

        for (int i = 0; i < bulletsCount; i++) {
            Vector3 shotInacurracyVector = new Vector3 (Random.Range (-shotInacurracy, shotInacurracy),
                                                        Random.Range (-shotInacurracy, shotInacurracy),
                                                        Random.Range (-shotInacurracy, shotInacurracy));

            Vector3 direction = raycastFrom.forward * range + shotInacurracyVector;

            if (Physics.Raycast (raycastFrom.position, direction, out RaycastHit hitInfo, range)) {
                if (hitInfo.rigidbody != null)
                    hitInfo.rigidbody.AddForce (-hitInfo.normal * impactForce / bulletsCount);

                SpawnImpactEffect (hitInfo);

                ITakeDamage hittedObject = hitInfo.transform.GetComponent<ITakeDamage> ();
                if (hittedObject != null && !hitInfo.transform.CompareTag("Player"))
                    hittedObject.TakeDamage (damage / bulletsCount);

            }
        }
    }

    private void SpawnImpactEffect (RaycastHit hitInfo) {
        GameObject ImpactEffectGO = Instantiate (impactEffectPrefab, hitInfo.point, Quaternion.LookRotation (hitInfo.normal));
        VisualEffect VFX = ImpactEffectGO.GetComponent<VisualEffect> ();
        if (VFX != null) {
            MeshRenderer targetMesh = hitInfo.transform.gameObject.GetComponentInChildren<MeshRenderer> ();
            if (targetMesh != null) {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForImpactEffect (targetMesh.material);
                if (gradient != null)
                    VFX.SetGradient ("Color", gradient);
            }
            VFX.Play ();
        }

        Destroy (ImpactEffectGO, 1f);
    }
}
