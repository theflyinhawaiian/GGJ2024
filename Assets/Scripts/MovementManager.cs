using System;
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

    public float Horizontal { get; private set;}
    public float Vertical { get; private set;}
    public float Speed { get; private set;}
    public bool IsSprinting { get; private set;}
    public delegate void JumpHandler (object sender, EventArgs e);
    public event JumpHandler JumpEvent;


    void Start()
    {
        ctrl = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (ctrl.isGrounded && !canJump)
        {
            playerVelocity.y = 0;
            canJump = true;
        }

        if (canJump && Input.GetButton("Jump"))
        {
            playerVelocity.y = jumpForce;
            canJump = false;
            JumpEvent?.Invoke (this, new EventArgs ());
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

        ctrl.Move(movementSpeed * Time.fixedDeltaTime * moveDirection);

        if (!ctrl.isGrounded)
        {
            playerVelocity.y += playerGravity * Time.fixedDeltaTime;
        }

        ctrl.Move(playerVelocity * Time.fixedDeltaTime);
    }
}