using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class Move_CharacterController : MonoBehaviour, IMove {
    private Vector3 moveVector;
    private Vector3 jumpVector;
    [SerializeField] float speed = 15f;

    public void SetJumpVector (Vector3 jumpVector) {
        this.jumpVector = jumpVector;
    }

    public void SetMoveVector (Vector3 moveVector) {
        this.moveVector = moveVector;
    }

    private CharacterController characterController;

    private void Awake () {
        characterController = GetComponent<CharacterController> ();
    }

    private void Update () {
        characterController.Move (moveVector * speed * Time.deltaTime);
        characterController.Move (jumpVector * Time.deltaTime);
    }
}
