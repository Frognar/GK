using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class Fireball : MonoBehaviour {
    public GameObject explosionVFX;
    [SerializeField] private int damage;
    private LayerMask targetList;
    private SoundPlayer soundPlayer;

    private void Awake () {
        soundPlayer = GetComponent<SoundPlayer> ();
        if (soundPlayer == null)
            Debug.LogWarning ("No soundPlayer on " + this.name);
    }

    private void Start () {
        StartCoroutine (LateStart());
    }

    IEnumerator LateStart () {
        yield return new WaitForEndOfFrame();

        soundPlayer.PlaySoundEvent ("Fireball");
    }

    public void SetTargetList (LayerMask targetList) {
        this.targetList = targetList;
    }

    void OnTriggerEnter (Collider other) {
        if (targetList == (targetList | (1 << other.gameObject.layer))) {
            soundPlayer.StopSoundEvent ("Fireball");
            ITakeDamage hittedObject = other.GetComponent<ITakeDamage> ();
            if (hittedObject != null)
                hittedObject.TakeDamage (damage);

            foreach (RaycastHit hit in Physics.SphereCastAll (transform.position, 15f, transform.forward)) {
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody> ();
                if (rb != null)
                    rb.AddExplosionForce (250f, transform.position, 15f);
            }

            GameObject explosion = Instantiate (explosionVFX, transform.position, transform.rotation);
            explosion.GetComponent<VisualEffect> ().Play ();

            StartCoroutine(LateExplosionSound(explosion.GetComponent<SoundPlayer> ()));
            Destroy (explosion, 3f);
            Destroy (gameObject, .1f);
        }
    }

    IEnumerator LateExplosionSound (SoundPlayer explosionSoundPlayer) {
        yield return new WaitForEndOfFrame ();

        explosionSoundPlayer?.PlaySoundEvent ("FireballExplosion");
    }
}
