using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float jumpForce;
    public float playerGravity;

    CharacterController ctrl;
    Vector3 playerVelocity = Vector3.zero;

    bool justGrounded = false;

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (ctrl.isGrounded && !justGrounded) {
            Debug.Log("Grounded!");
            justGrounded = true;
        }

        if(ctrl.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (ctrl.isGrounded && Input.GetButtonDown("Jump")) {
            playerVelocity.y = jumpForce;
            justGrounded = false;
        }

        var vert = Input.GetAxis("Vertical");
        var hor = Input.GetAxis("Horizontal");

        var move = new Vector3(-hor, 0 , -vert);

        ctrl.Move(move);

        if (!ctrl.isGrounded) {
            playerVelocity.y += playerGravity * Time.fixedDeltaTime;
            ctrl.Move(playerVelocity);
        }
    }
}
