using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravityForce = -30f;
    
    [Header("Jumping")]
    [SerializeField] private float jumpHight = 3f;
    [SerializeField] private float groundDistance = 0.4f;
    private bool isGrounded = true;
    public Transform groundCheck;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    private CharacterController controller;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player.IsAlive)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHight * -2 * gravityForce);

            velocity.y += gravityForce * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
