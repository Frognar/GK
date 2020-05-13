using UnityEngine.VFX;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class Shoot_Gun: MonoBehaviour, IShoot
{
    [SerializeField] private GunPattern gunPattern;

    private Transform raycastFrom;

    [SerializeField] private GameObject impactEffectPrefab;
    private VisualEffect shotEffect;
    private Animator animator;

    private SoundPlayer soundPlayer;

    public float FireRate => 1f / gunPattern.FireRate;

    public int PixelCost => gunPattern.PixelCost;

    private void Start () {
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
        soundPlayer?.PlaySoundEvent(gunPattern.ShotSoundName.ToString());
    }

    public void Shoot () {
        ShotEffectPlay ();

        for (int i = 0; i < gunPattern.BulletsCount; i++) {
            Vector3 shotInacurracyVector = new Vector3 (Random.Range (-gunPattern.ShotInacurracy, gunPattern.ShotInacurracy),
                                                        Random.Range (-gunPattern.ShotInacurracy, gunPattern.ShotInacurracy),
                                                        Random.Range (-gunPattern.ShotInacurracy, gunPattern.ShotInacurracy));

            Vector3 direction = raycastFrom.forward * gunPattern.Range + shotInacurracyVector;

            if (Physics.Raycast (raycastFrom.position, direction, out RaycastHit hitInfo, gunPattern.Range)) {
                if (hitInfo.rigidbody != null)
                    hitInfo.rigidbody.AddForce (-hitInfo.normal * gunPattern.ImpactForce / gunPattern.BulletsCount);

                SpawnImpactEffect (hitInfo);

                ITakeDamage hittedObject = hitInfo.transform.GetComponent<ITakeDamage> ();
                if (hittedObject != null && !hitInfo.transform.CompareTag("Player"))
                    hittedObject.TakeDamage (gunPattern.Damage / gunPattern.BulletsCount);

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
