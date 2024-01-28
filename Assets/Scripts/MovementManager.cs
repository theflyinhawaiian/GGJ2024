using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private Transform playerCamera;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            playerVelocity.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y += jumpForce;
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime); // Apply gravity

        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = GetCameraRelativeDirection(horizontalInput, verticalInput);

        controller.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

    private Vector3 GetCameraRelativeDirection(float horizontal, float vertical)
    {
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        return forward * vertical + right * horizontal;
    }
}
