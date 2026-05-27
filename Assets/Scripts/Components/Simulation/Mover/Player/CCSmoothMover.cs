using UnityEngine;

[CreateAssetMenu(fileName = "CC Smooth Mover", menuName = "Components/Simulation/Mover/CharacterController/CC Smooth Mover")]
public class CCSmoothMover : MoverSO
{
    public override void Move(Unit unit, Vector3 moveDir, float dt)
    {
        MovementStats stats = unit.Stats.GetStats(this);
        bool hasInput = moveDir.sqrMagnitude > 0.001f;

        if (hasInput)
        {
            unit.State.MoveState.CurrentMoveDirection = moveDir.normalized;
        }

        UpdateSpeed(stats, unit.State.MoveState, hasInput, dt);

        Vector3 targetVelocity = hasInput ? unit.State.MoveState.CurrentMoveDirection * stats.MaxSpeed : Vector3.zero;
        float rate = hasInput ? stats.Acceleration : stats.Deceleration;

        unit.State.MoveState.MovementVelocity = Vector3.MoveTowards(unit.State.MoveState.MovementVelocity, targetVelocity, rate * dt);
        Vector3 finalVelocity = unit.State.MoveState.MovementVelocity + unit.State.MoveState.ExternalForcesVelocity;
        unit.Refs.CC.Move(finalVelocity * dt);
    }
}