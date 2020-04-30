using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    private IMove move;
    private HeightController jumpController;

    private void OnDisable () {
        move.SetMoveVector (Vector3.zero);
        move.SetJumpVector (Vector3.zero);
    }

    private void Awake () {
        move = GetComponent<IMove> ();
        if (move == null)
            Debug.LogError ("No movement script in Player!");

        jumpController = GetComponent<HeightController> ();
        if (jumpController == null)
            Debug.LogError ("No jump controller script in Player!");
    }

    private void PlayerMovement () {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;
        move.SetMoveVector (direction);

        Vector3 jump = jumpController.HeightControl (Input.GetButtonDown ("Jump"));
        move.SetJumpVector (jump);
    }

    private void Update () {
        PlayerMovement ();
    }
}
