using UnityEngine;
using static UnityEditor.FilePathAttribute;

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

        //if (!ctrl.isGrounded)
        //{
        //    playerVelocity.y += playerGravity * Time.fixedDeltaTime;
        //}

        playerVelocity.y += playerGravity * Time.fixedDeltaTime;
        ctrl.Move(playerVelocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Respawn")
            return;

        // character controller is a tyrant with no respect for us, so we need to kill it for a sec to transport our player back to spawn
        ctrl.enabled = false;
        gameObject.transform.position = new Vector3(-1, 4, 1);
        ctrl.enabled = true;

        playerVelocity.y = 0;
    }
}
