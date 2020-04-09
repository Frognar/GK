using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_speed = 12f;
    [SerializeField] private float m_gravityForce = -30f;
    [SerializeField] private float m_jumpHight = 3f;
    public float speed { get { return m_speed; } set { m_speed = value; } }
    public float gravityForce { get { return m_gravityForce; } set { m_gravityForce = value; } }
    public float jumpHight { get { return m_jumpHight; } set { m_jumpHight = value; } }

    public float groundDistance = 0.4f;
    private bool isGrounded = true;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;
    private CharacterController controller;

    [Header("Camera")]
    public GameObject playerCamera;
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private Player playerInstance;

    void Start()
    {
        playerInstance = GetComponent<Player>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (playerInstance.isAlive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

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
