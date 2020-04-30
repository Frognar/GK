using UnityEngine;

public class HeightController : MonoBehaviour {
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravityForce = -30f;
    private bool isGrounded = true;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;


    private float sphereRadius = 0.5f;
    [Header("For NPCs")]
    public bool iAmNPC = true;
    [SerializeField] private Vector3 halfExtents = new Vector3 (5f, .5f, .5f);

    public Vector3 HeightControl (bool jumpRequest) {
        if (iAmNPC)
            isGrounded = Physics.CheckBox (groundCheck.position, halfExtents, transform.rotation, groundMask);
        else
            isGrounded = Physics.CheckSphere (groundCheck.position, sphereRadius, groundMask);

        if (isGrounded) {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (jumpRequest)
                velocity.y = Mathf.Sqrt (-2 * jumpForce * gravityForce);
        }
        
        velocity.y += gravityForce * Time.deltaTime;

        return velocity;
    }

    private void OnDrawGizmos () {
        if (iAmNPC) {
            Gizmos.matrix = groundCheck.localToWorldMatrix;
            Vector3 cubeSize = new Vector3 (halfExtents.x * 2 / transform.localScale.x,
                                            halfExtents.y * 2 / transform.localScale.y,
                                            halfExtents.z * 2 / transform.localScale.z);
            Gizmos.DrawWireCube (Vector3.zero, cubeSize);
        } else
            Gizmos.DrawWireSphere (groundCheck.position, sphereRadius);
    }
}
