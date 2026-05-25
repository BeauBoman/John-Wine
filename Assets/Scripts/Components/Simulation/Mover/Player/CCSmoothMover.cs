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
        Vector3 horizontalVelocity = unit.State.MoveState.CurrentMoveDirection * unit.State.MoveState.CurrentSpeed;
        Vector3 finalVelocity = horizontalVelocity + unit.State.MoveState.ExternalForcesVelocity;

        unit.Refs.CC.Move(finalVelocity * dt);
    }
}