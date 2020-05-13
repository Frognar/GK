using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class Move_Rigidbody : MonoBehaviour, IMove {
    #region IMove
    public void SetDirectionVector (Vector3 directionVector) {
        this.directionVector = directionVector;
    }

    public void Jump () {
        if (Physics.CheckSphere (groundChecker.position, moveStats.GroundDistance, moveStats.AllowJumpOn, QueryTriggerInteraction.Ignore)) {
            rigidBody.AddForce (Vector3.up * Mathf.Sqrt (moveStats.JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }
    #endregion

    private Rigidbody rigidBody;
    [SerializeField] private MoveStats moveStats;
    private Vector3 directionVector = Vector3.zero;
    private Transform groundChecker;

    private void Awake () {
        rigidBody = GetComponent<Rigidbody> ();
        groundChecker = transform.Find ("GroundChecker");
        if(moveStats == null) {
            Debug.LogError ("Nie podłączono MoveStats do skryptu Move_Rigidbody. [" + this.name + "]");
        }
        if(rigidBody == null) {
            Debug.LogError ("Brakuje komponentu Rigidbody dla skryptu Move_Rigidbody. [" + this.name + "]");
        }
        if(groundChecker == null) {
            Debug.LogError ("Brakuje elementu 'GroundChecker' dla skryptu Move_Rigidbody. [" + this.name + "]");
        }

        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate () {
        rigidBody.MovePosition (rigidBody.position + directionVector * moveStats.Speed * UnityEngine.Time.fixedDeltaTime);
    }
}
