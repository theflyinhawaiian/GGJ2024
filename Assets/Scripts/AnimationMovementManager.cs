using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 5.0f; // Speed of rotation
    public Transform playerCamera;
    public Animator animator;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float gravityValue = -9.81f;
    private float currentSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Get input and calculate movement direction in relation to camera
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 moveDirection = forward * move.z + right * move.x;

        // Check if there is movement to determine if the character should rotate
        if (moveDirection != Vector3.zero)
        {
            // Calculate target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            // Smoothly rotate towards the target rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Determine if the character is sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Move the character
        controller.Move(currentSpeed * Time.deltaTime * moveDirection);

        // Update animator parameters
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetBool("IsSprinting", isSprinting);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
