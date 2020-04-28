using UnityEngine;

public class JumpController : MonoBehaviour {
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravityForce = -30f;
    [SerializeField] private float groundDistance = 0.4f;
    private bool isGrounded = true;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity = new Vector3 ();

    public Vector3 jump (bool spacePressed) {
        isGrounded = Physics.CheckSphere (groundCheck.position, groundDistance, groundMask);

        if (isGrounded) {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (spacePressed)
                velocity.y = Mathf.Sqrt (-2 * jumpForce * gravityForce);
        }

        velocity.y += gravityForce * Time.deltaTime;

        return velocity;
    }
}
