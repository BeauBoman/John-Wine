using System;
using UnityEngine;

public class PlayerController : Unit, IUpdatable
{
    public Transform CameraTransform;
    public float PushPower = 2;

    private void Start() => Registerer.RegisterUpdatable(this);

    public void OnUpdate(float dt)
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 moveDir = ConvertToCameraSpace(input);

        HandleGravity(dt);
        HandleJump();

        Components.Mover.Move(this, moveDir, dt);
    }
    private void HandleGravity(float dt)
    {
        if (Refs.CC.isGrounded && State.ExternalForcesVelocity.y < 0)
        {
            State.ExternalForcesVelocity.y = -2f;
        }
        State.ExternalForcesVelocity.y += Stats.Movement.Gravity * dt;
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Refs.CC.isGrounded)
        {
            State.ExternalForcesVelocity.y = Mathf.Sqrt(Stats.Movement.JumpForce * -2f * Stats.Movement.Gravity);
        }
    }
    private Vector3 ConvertToCameraSpace(Vector3 input)
    {
        Vector3 forward = CameraTransform.forward;
        Vector3 right = CameraTransform.right;
        forward.y = 0; right.y = 0;
        return (forward.normalized * input.z + right.normalized * input.x).normalized;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;
        if (hit.moveDirection.y < -0.3) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.linearVelocity = pushDir * PushPower;
    }
}
