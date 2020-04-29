using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    [Header ("Attack")]
    [SerializeField] private float attackRadius = 25f;
    public float AttackRadius {
        get {
            return attackRadius;
        }
    }
    [SerializeField] private float attackRate = 2f;
    public float AttackRate {
        get {
            return 1 / attackRate;
        }
    }
    private Transform target;
    public Vector3 TargetPosition {
        get {
            return target.position;
        }
    }

    public GameObject missle;
    [SerializeField] private float missleSpeed = 20f;

    private void Start () {
        target = PlayerManager.instance.player.transform;
    }

    public void AttackTarget () {
        GameObject fireBall = Instantiate (missle, transform.position, transform.rotation);
        Rigidbody rb = fireBall.GetComponent<Rigidbody> ();

        if (rb != null)
            rb.velocity = (target.transform.position - transform.position).normalized * missleSpeed;

        Destroy (fireBall, 15f);
    }
}
