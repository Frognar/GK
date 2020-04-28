using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingMoveAround : MonoBehaviour {
    private IMove move;
    [SerializeField] private float rayCastDistance = 10f;
    [SerializeField] int rayCount = 5;

    [SerializeField] private float angularSpeed = 10f;
    private float movementTimeRemaining = 0f;

    private JumpController jumpController;
    [SerializeField] private bool allowJump = false;
    private bool jumpNow = false;
    [SerializeField] private float timeBeetwenJumps = 5f;
    [SerializeField] private float jumpProbability = 0.5f;
    private float lastJampTime = 0f;

    private Vector3 targetPosition;

    private void Awake () {
        move = GetComponent<IMove> ();
        if (move == null)
            Debug.LogError ("No moveVelocity script on NPC [" + this.name + "]");

        jumpController = GetComponent<JumpController> ();
        if (jumpController == null)
            Debug.LogError ("No jumpController script on NPC [" + this.name + "]");
    }

    private bool ChangePath () {
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

    void Rotate (float directionX, float directionZ) {
        Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (directionX, 0, directionZ));
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void SetTargerPosition (Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    void RandomMovement () {
        if (movementTimeRemaining <= 0) {
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

            Vector3 direction = transform.forward.normalized;
            direction.y = 0f;
            
            move.SetMoveVector (direction);
        }
    }

    private void Jump () {
        if (allowJump) {

            if (Time.time >= lastJampTime) {
                jumpNow = Random.Range (0f, 1f) < jumpProbability;
                lastJampTime = Time.time + timeBeetwenJumps;
            } else
                jumpNow = false;
            Vector3 jump = jumpController.jump (jumpNow);
            move.SetJumpVector (jump);
        } else
            move.SetJumpVector (-Vector3.up);
    }

    void Update () {
        move.SetMoveVector (Vector3.zero);
        if (targetPosition == Vector3.zero)
            RandomMovement ();
        else {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0f;
            move.SetMoveVector (direction);
        }

        Jump ();
    }
}
