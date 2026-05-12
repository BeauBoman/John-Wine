using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IUpdatable
{
    [SerializeField] private Unit Unit;
    public Transform CameraTransform;
    public Transform FirePoint;
    public float PushPower = 2;

    private void Start()
    {
        Registerer.RegisterUpdatable(this);
    }

    public void OnUpdate(float dt)
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 moveDir = ConvertToCameraSpace(input);

        HandleGravity(dt);
        HandleJump();
        HandleWeapon();

        Unit.UnitStats.SimComponents.Mover.Move(Unit, moveDir, dt);
    }
    private void HandleGravity(float dt)
    {
        if (Unit.Refs.CC.isGrounded && Unit.Stats.MoveState.ExternalForcesVelocity.y < 0)
        {
            Unit.Stats.MoveState.ExternalForcesVelocity.y = -2f;
        }
        Unit.Stats.MoveState.ExternalForcesVelocity.y += Unit.Stats.Movement.Gravity * dt;
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Unit.Refs.CC.isGrounded)
        {
            Unit.Stats.MoveState.ExternalForcesVelocity.y = Mathf.Sqrt(Unit.Stats.Movement.JumpForce * -2f * Unit.Stats.Movement.Gravity);
        }
    }
    private void HandleWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Unit.UnitStats.Weapon.Fire(new PositionArgs(FirePoint.position, FirePoint.rotation));
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
