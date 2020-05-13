using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
[RequireComponent (typeof (CharacterController))]
public class Move_CharacterController : MonoBehaviour, IMove {
    #region IMove
    public void SetDirectionVector (Vector3 directionVector) {
        this.directionVector = directionVector;
    }

    public void Jump () {
        if (isGrounded) {
            directionVector.y += Mathf.Sqrt (moveStats.JumpHeight * -2f * gravity);
        }
    }
    #endregion
    private CharacterController characterController;
    [SerializeField] private MoveStats moveStats;
    [SerializeField] private Vector3 drag = Vector3.one * 8;
    [SerializeField] private float gravity = -29.43f;
    private Vector3 directionVector;
    private Vector3 velocity;
    private bool isGrounded = true;
    private Transform groundChecker;

    private void Awake () {
        characterController = GetComponent<CharacterController> ();
        groundChecker = transform.Find ("GroundChecker");
        if (moveStats == null) {
            Debug.LogError ("Nie podłączono MoveStats do skryptu Move_CharacterController. [" + this.name + "]");
        }
        if (characterController == null) {
            Debug.LogError ("Brakuje komponentu CharacterController dla skryptu Move_CharacterController. [" + this.name + "]");
        }
        if (groundChecker == null) {
            Debug.LogError ("Brakuje elementu 'GroundChecker' dla skryptu Move_CharacterController. [" + this.name + "]");
        }
    }

    private void Update () {
        isGrounded = Physics.CheckSphere (groundChecker.position, moveStats.GroundDistance, moveStats.AllowJumpOn, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0f) {
            velocity.y = 0f;
        }

        characterController.Move (directionVector * Time.deltaTime * moveStats.Speed);
        velocity.y += gravity * Time.deltaTime;
        velocity.x /= 1 + drag.x * Time.deltaTime;
        velocity.y /= 1 + drag.y * Time.deltaTime;
        velocity.z /= 1 + drag.z * Time.deltaTime;
        characterController.Move (velocity * Time.deltaTime);
    }
}
