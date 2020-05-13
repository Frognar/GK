using UnityEngine;

/**
 * Author:          Anna Mach
 * Collaborators:   Sebastian Przyszlak - wykorzystanie IMove
 */
public class MoveAround : MonoBehaviour {
    protected IMove move;
    [SerializeField] private float rayCastDistance = 10f;
    [SerializeField] private int rayCount = 5;

    [SerializeField] private float angularSpeed = 10f;
    private float movementTimeRemaining = 0f;

    [SerializeField] private bool allowJump = false;
    private bool jumpNow = false;
    [SerializeField] private float timeBeetwenJumps = 5f;
    [SerializeField] private float jumpProbability = 0.5f;
    private float lastJampTime = 0f;

    protected virtual void Awake () {
        move = GetComponent<IMove> ();
        if (move == null)
            Debug.LogError ("No IMove script on NPC [" + this.name + "]");
    }

    protected bool ChangePath () {
        for (float i = -5; i <= 5; i += 10f / (rayCount - 1)) {
            Ray ray = new Ray (transform.position - transform.right * i, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, rayCastDistance)) {
                if (hit.transform == this.transform)
                    continue;
                return true;
            }
        }
        return false;
    }

    protected void Rotate (float xRotation, float zRotation) {
        Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (xRotation, 0, zRotation));
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
    }

    protected void RandomMovement () {
        if (movementTimeRemaining <= 0f) {
            move.SetDirectionVector (Vector3.zero);
            System.Random rnd = new System.Random ((int) (transform.position.x + transform.position.y + transform.position.z + Time.time));
            int fate = rnd.Next (0, 100);
            if (fate > 10 & fate < 35)
                Rotate (rnd.Next (-10, 10), rnd.Next (-10, 10));
            else if (fate > 60 & fate < 90)
                movementTimeRemaining = rnd.Next (2, 4);
        } else {
            movementTimeRemaining -= Time.deltaTime;
            if (ChangePath ())
                Rotate (transform.right.x, transform.right.z);

            Vector3 movementVector = transform.forward;
            movementVector.y = 0f;

            move.SetDirectionVector (movementVector);
        }
    }

    protected void RandomJump () {
        if (allowJump) {
            if (Time.time >= lastJampTime) {
                jumpNow = Random.Range (0f, 1f) < jumpProbability;
                lastJampTime = Time.time + timeBeetwenJumps;
            } else {
                jumpNow = false;
            }
            
            if (jumpNow) {
                move.Jump ();
            }
        }
    }

    protected virtual void Update () {
        RandomMovement ();
        RandomJump ();
    }
}
