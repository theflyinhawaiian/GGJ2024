using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float jumpHeight = 2.0f;
    public float rotationSpeed = 5.0f; 
    public Transform playerCamera;
    public Animator animator;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded = true;
    private float gravityValue = -9.81f;
    private float currentSpeed;

    private MovementManager moveManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        moveManager = GetComponent<MovementManager>();
        moveManager.JumpEvent += (obj, e) =>
        {
            animator.SetTrigger("Jump");
            Debug.Log("Jump Triggered!"); // Debugging line
        };
    }

    private void Update()
    {
        //isGrounded = controller.isGrounded;
        //Debug.Log($"Is Grounded: {isGrounded}, Velocity Y: {playerVelocity.y}");

        //if (isGrounded)
        //{
        //    if (playerVelocity.y < 0)
        //    {
        //        playerVelocity.y = 0f;
        //    }
        //}

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Vector3 forward = playerCamera.forward;
        //Vector3 right = playerCamera.right;
        //forward.y = 0;
        //right.y = 0;
        //forward.Normalize();
        //right.Normalize();
        //Vector3 moveDirection = forward * move.z + right * move.x;

        //if (moveDirection != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //}

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Set animator parameters
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", moveManager.Speed);
        animator.SetBool("IsSprinting", isSprinting);

        //if (Input.GetButton("Jump") && isGrounded)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //    animator.SetTrigger("Jump");
        //    Debug.Log("Jump Triggered!"); // Debugging line
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
        //controller.Move(currentSpeed * Time.deltaTime * moveDirection);
    }
}
