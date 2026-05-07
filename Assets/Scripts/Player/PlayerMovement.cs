using System;
using UnityEngine;

public class PlayerMovement : Unit
{
    public Transform CameraObject;
    public CharacterController Controller;
    public float Gravity = -9.81f;
    public float JumpForce = 3f;
    public float speed;
    private Vector3 velocity;
    private Vector3 moveDir;
    public void Update()
    {
        if (Controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpForce * -2f * Gravity);
        }

        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) input.z += 1;
        if (Input.GetKey(KeyCode.S)) input.z -= 1;
        if (Input.GetKey(KeyCode.D)) input.x += 1;
        if (Input.GetKey(KeyCode.A)) input.x -= 1;

        Vector3 forward = CameraObject.forward;
        Vector3 right = CameraObject.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDir = (forward * input.z + right * input.x).normalized;

        velocity.y += Gravity * Time.deltaTime;

        Vector3 finalMove = (moveDir * speed + velocity);
        Controller.Move(finalMove * Time.deltaTime);
    }

    public float pushPower = 2.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;
        if (hit.moveDirection.y < -0.3) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.linearVelocity = pushDir * pushPower;
    }
}
   