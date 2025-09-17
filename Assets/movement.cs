using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = 20f;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController not found on this GameObject.");
            enabled = false; // Disable the script if no CharacterController is present
        }
    }

    void Update()
    {
        // Grounded check and horizontal movement
        if (controller.isGrounded)
        {
            // Get input for horizontal and vertical movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate movement direction relative to the player's forward direction
            moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
            moveDirection *= moveSpeed;

            // Jump input
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            // Apply gravity when not grounded
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Apply movement
        controller.Move(moveDirection * Time.deltaTime);
    }
}