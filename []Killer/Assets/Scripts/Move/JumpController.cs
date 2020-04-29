using UnityEngine;

public class JumpController : MonoBehaviour {
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravityForce = -30f;
    private bool isGrounded = true;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity = new Vector3 ();


    private float sphereRadius = 0.5f;
    [Header("For Enemy")]
    public bool enemy = true;
    [SerializeField] private Vector3 halfExtents = new Vector3 (5f, .5f, .5f);

    public Vector3 jump (bool spacePressed) {
        if (enemy)
            isGrounded = Physics.CheckSphere (groundCheck.position, sphereRadius, groundMask);
        else
            isGrounded = Physics.CheckBox (groundCheck.position, halfExtents, transform.rotation, groundMask);

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
