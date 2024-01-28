using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float jumpForce;
    public float playerGravity;
    public float movementSpeed = 5.0f;
    public float sprintSpeed = 10.0f; // Sprint speed
    public Transform playerCamera;

    private CharacterController ctrl;
    private Vector3 playerVelocity = Vector3.zero;
    private bool canJump = true;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public float Speed { get; private set; }
    public bool IsSprinting { get; private set; }
    public delegate void JumpHandler(object sender, EventArgs e);
    public event JumpHandler JumpEvent;

    private bool jumpRequested = false;
    private float jumpCooldown = 0.2f; // 200ms cooldown
    private float lastJumpTime = -1f;

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
    }

    void Update() // Capture input in Update
    {
        if (Input.GetButton("Jump") && Time.time > lastJumpTime + jumpCooldown)
        {
            jumpRequested = true;
        }

        // Handle sprint input
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    void FixedUpdate()
    {
        if (ctrl.isGrounded && !canJump)
        {
            playerVelocity.y = 0;
            canJump = true;
        }

        if (canJump && jumpRequested)
        {
            playerVelocity.y = jumpForce;
            canJump = false;
            jumpRequested = false; // Reset the jump request
            lastJumpTime = Time.time; // Update the last jump time
            JumpEvent?.Invoke(this, new EventArgs());
        }

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

        float currentSpeed = IsSprinting ? sprintSpeed : movementSpeed; // Choose speed based on sprinting state

        ctrl.Move(currentSpeed * Time.fixedDeltaTime * moveDirection);

        if (!ctrl.isGrounded)
        {
            playerVelocity.y += playerGravity * Time.fixedDeltaTime;
        }

        ctrl.Move(playerVelocity * Time.fixedDeltaTime);
    }
}
