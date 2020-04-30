using UnityEngine;

public class HeightController : MonoBehaviour {
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravityForce = -30f;
    private Vector3 heightVector;

    private bool IsGrounded {
        get {
            return Physics.CheckBox (groundCheck.position, groundCheckHalfExtents, transform.rotation, groundMask);
        }
    }
    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField] private Vector3 groundCheckHalfExtents = new Vector3 (5f, 1f, 1f);

    public Vector3 HeightControl (bool jumpRequest) {
        if (IsGrounded) {
            if (heightVector.y < 0)
                heightVector.y = -1f;

            if (jumpRequest)
                heightVector.y = Mathf.Sqrt (-2 * jumpForce * gravityForce);
        }
        
        heightVector.y += gravityForce * Time.deltaTime;

        return heightVector;
    }

    private void OnDrawGizmos () {
        Gizmos.matrix = groundCheck.localToWorldMatrix;
        Gizmos.DrawWireCube (Vector3.zero, groundCheckHalfExtents * 2);
    }
}
