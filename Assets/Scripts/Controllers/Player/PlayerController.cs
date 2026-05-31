using System;
using System.Collections;
using UnityEngine;

public sealed class PlayerController : Controller, IUpdatable
{
    [SerializeField] private Unit _unit;
    public Transform CameraTransform;
    public Transform FirePoint;
    public float PushPower = 2;

    public float CoyoteTime;
    public float JumpBufferTime;
    private float _coyoteTime;
    private float _jumpBufferTime;
    private ModifiableStats<MovementStats> moveStats;
    private void Start() => _unit.OnSpawn();
    public override void OnStart()
    {
        _unit.OnHealthIsZero += Death;
        Registerer.RegisterUpdatable(this);
        moveStats = _unit.Stats.GetStatsModifiable(_unit.UnitSO.SimComponents.Mover);
        _unit.ChangeAbility(0);

        //StartCoroutine(BuffTest());
    }
    private IEnumerator BuffTest()
    {
        yield return new WaitForSeconds(5);
        _unit.Stats.GetStatsModifiable(_unit.UnitSO.SimComponents.Mover).BuffAdd(new MovementStats() { Acceleration = -35, Deceleration = -35 });
        Debug.Log("Buff applied. You're on ice now lol");
    }
    public void OnUpdate(float dt)
    {
        _unit.OnUpdate(dt);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 moveDir = ConvertToCameraSpace(input);

        HandleGravity(dt);
        HandleJump(dt);
        HandleWeapon();

        _unit.UnitSO.SimComponents.Mover.Move(_unit, moveDir, dt);

        _unit.State.CurrentAbility.ReloadProgress(dt);
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
            _coyoteTime = CoyoteTime;
        else
            _coyoteTime -= dt;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpBufferTime = JumpBufferTime;
        }

        if (_jumpBufferTime > 0)
            _jumpBufferTime -= dt;

        if (_jumpBufferTime > 0 && _unit.Refs.CC.isGrounded == true)
        {
            _unit.State.MoveState.ExternalForcesVelocity.y = Mathf.Sqrt(moveStats.Value.JumpForce * -2f * moveStats.Value.Gravity);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _coyoteTime > 0)
        {
            _unit.State.MoveState.ExternalForcesVelocity.y = Mathf.Sqrt(moveStats.Value.JumpForce * -2f * moveStats.Value.Gravity);
        }
    }
    private void HandleWeapon()
    {
        if (Input.GetMouseButton(0))
        {
            if (_unit.State.CurrentAbility.CanShoot == false) return;

            _unit.State.CurrentAbility.Fire(new PositionArgs(FirePoint.position, FirePoint.rotation), _unit);
            _unit.State.CurrentAbility.ResetReloadProgress();
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
