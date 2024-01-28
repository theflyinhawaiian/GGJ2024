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

    private MovementManager moveManager;

    private void Start()
    {
        moveManager = GetComponent<MovementManager>();
        moveManager.JumpEvent += (obj, e) =>
        {
            animator.SetTrigger("Jump");
            Debug.Log("Jump Triggered!"); // Debugging line
        };
        animator.ResetTrigger("Jump");
    }

    private void Update()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", moveManager.Speed);
        animator.SetBool("IsSprinting", isSprinting);
    }
}
