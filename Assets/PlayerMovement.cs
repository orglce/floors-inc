using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

public CharacterController controller;
public float speed = 12f;

[SerializeField] public float gravity = -2f;
public float jumpHeight = 1f;

Vector3 velocity;
bool isGrounded;

public Transform groundCheck;
public float groundDistance = 0.4f;
public LayerMask groundMask;

void Update()
{
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
                velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        Vector3 moveVector = transform.right * x + transform.forward * z;

        controller.Move(moveVector * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
}
}
