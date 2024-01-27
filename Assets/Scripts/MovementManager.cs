using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float jumpForce;
    public float playerGravity;
    public float movementSpeed = 5.0f;

    CharacterController ctrl;
    Vector3 playerVelocity = Vector3.zero;

    bool canJump = true;

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (ctrl.isGrounded && !canJump) {
            playerVelocity.y = 0;
            canJump = true;
        }

        if (canJump && Input.GetButton("Jump")) {
            playerVelocity.y = jumpForce;
            canJump = false;
        }

        var vert = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        var move = new Vector3(-hor, 0 , -vert) * movementSpeed;

        ctrl.Move(move);

        if (!ctrl.isGrounded) {
            playerVelocity.y += playerGravity * Time.fixedDeltaTime;
        }

        
        ctrl.Move(playerVelocity);
    }
}
