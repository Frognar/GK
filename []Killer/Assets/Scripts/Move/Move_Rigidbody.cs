using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class Move_Rigidbody : MonoBehaviour, IMove {
    private Vector3 moveVector;
    private Vector3 jumpVector;
    [SerializeField] float speed = 15f;

    public void SetJumpVector (Vector3 jumpVector) {
        this.jumpVector = jumpVector;
    }

    public void SetMoveVector (Vector3 moveVector) {
        this.moveVector = moveVector;
    }

    private Rigidbody rigidBody;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody> ();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate () {
        rigidBody.velocity = moveVector * speed;
        rigidBody.velocity += jumpVector;
    }
}
