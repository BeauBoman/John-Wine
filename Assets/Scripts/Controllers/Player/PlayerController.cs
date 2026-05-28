using System;
using UnityEngine;

public sealed class PlayerController : Controller, IUpdatable
{
    [SerializeField] private Unit _unit;
    public Transform CameraTransform;
    public Transform FirePoint;
    public float PushPower = 2;
    private Ability _ability;

    private float _coyoteTime;
    private float _jumpBufferTime;
    private ModifiableStats<MovementStats> moveStats;
    private void Start() => _unit.OnSpawn();
    public override void OnStart()
    {
        _ability = _unit.State.CurrentAbility;
        _unit.OnHealthIsZero += Death;
        Registerer.RegisterUpdatable(this);
        moveStats = _unit.Stats.GetStatsModifiable(_unit.UnitSO.SimComponents.Mover);
    }

    public void OnUpdate(float dt)
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 moveDir = ConvertToCameraSpace(input);

        HandleGravity(dt);
        HandleJump(dt);
        HandleWeapon();

        _unit.UnitSO.SimComponents.Mover.Move(_unit, moveDir, dt);

        if (_ability != null)
        {
            _ability.ReloadProgress(dt);
        }
    }
    private void HandleGravity(float dt)
    {
        if (_unit.Refs.CC.isGrounded && _unit.State.MoveState.ExternalForcesVelocity.y < 0)
        {
            _unit.State.MoveState.ExternalForcesVelocity.y = -2f;
        }
        _unit.State.MoveState.ExternalForcesVelocity.y += _unit.Stats.GetStats(_unit.UnitSO.SimComponents.Mover).Gravity * dt;
    }
    private void HandleJump(float dt)
    {
        if (_unit.Refs.CC.isGrounded == true)
            _coyoteTime = 0.2f;
        else
            _coyoteTime -= dt;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpBufferTime = 0.2f;
        }

        if (_jumpBufferTime > 0)
            _jumpBufferTime -= dt;

        if (_jumpBufferTime > 0 && _unit.Refs.CC.isGrounded == true)
        {
            _unit.State.MoveState.ExternalForcesVelocity.y = Mathf.Sqrt(_unit.Stats.GetStats(_unit.UnitSO.SimComponents.Mover).JumpForce * -2f * _unit.Stats.GetStats(_unit.UnitSO.SimComponents.Mover).Gravity);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _coyoteTime > 0)
        {
            _unit.State.MoveState.ExternalForcesVelocity.y = Mathf.Sqrt(_unit.Stats.GetStats(_unit.UnitSO.SimComponents.Mover).JumpForce * -2f * _unit.Stats.GetStats(_unit.UnitSO.SimComponents.Mover).Gravity);
        }
    }
    private void HandleWeapon()
    {
        if (Input.GetMouseButton(0))
        {
            if (_ability.CanShoot == false) return;

            _unit.UnitSO.SimComponents.Ability.Fire(new PositionArgs(FirePoint.position, FirePoint.rotation), _unit);
            _ability.ResetReloadProgress();
        }
    }
    private Vector3 ConvertToCameraSpace(Vector3 input)
    {
        Vector3 forward = CameraTransform.forward;
        Vector3 right = CameraTransform.right;
        forward.y = 0; right.y = 0;

        forward.Normalize();
        right.Normalize();

        return Vector3.ClampMagnitude(forward * input.z + right * input.x, 1f);
    }

    public void Death()
    {
        _unit.OnHealthIsZero -= Death;
        Registerer.UnregisterUpdatable(this);
        Destroy(gameObject);
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
