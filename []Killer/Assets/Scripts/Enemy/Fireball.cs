using UnityEngine;
using UnityEngine.VFX;

public class Fireball : MonoBehaviour {
    public GameObject explosionVFX;
    [SerializeField] private int damage;
    private LayerMask targetList;
    public void SetTargetList (LayerMask targetList) {
        this.targetList = targetList;
    }

    void OnTriggerEnter (Collider other) {
        if (Physics.CheckSphere (transform.position, 1f, targetList) && !other.CompareTag ("Ignore")) {
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
            Destroy (explosion, 3f);
            Destroy (gameObject, 0.01f);
        }
    }
}
