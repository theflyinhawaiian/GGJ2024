using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float jumpForce;
    public float playerGravity;
    public float movementSpeed = 5.0f;
    public Transform playerCamera;

    private CharacterController ctrl;
    private Vector3 playerVelocity = Vector3.zero;
    private bool canJump = true;

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (ctrl.isGrounded && !canJump)
        {
            playerVelocity.y = 0;
            canJump = true;
        }

        if (canJump && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = jumpForce;
            canJump = false;
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * z + right * x;

        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();

        ctrl.Move(movementSpeed * Time.fixedDeltaTime * moveDirection);

        playerVelocity.y += playerGravity * Time.fixedDeltaTime;
        ctrl.Move(playerVelocity * Time.fixedDeltaTime);
    }
}
