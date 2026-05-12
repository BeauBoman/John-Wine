using UnityEngine;

[CreateAssetMenu(fileName = "CC Smooth Mover", menuName = "Components/Simulation/Mover/CharacterController/CC Smooth Mover")]
public class CCSmoothMover : Mover
{
    public override void Move(Unit unit, Vector3 moveDir, float dt)
    {
        MovementStats stats = unit.Stats.Movement;

        bool hasInput = moveDir.sqrMagnitude > 0.001f;

        if (hasInput)
        {
            unit.Stats.MoveState.CurrentMoveDirection = moveDir.normalized;
        }

        UpdateSpeed(stats, unit.Stats.MoveState, hasInput, dt);
        Vector3 horizontalVelocity = unit.Stats.MoveState.CurrentMoveDirection * unit.Stats.MoveState.CurrentSpeed;
        Vector3 finalVelocity = horizontalVelocity + unit.Stats.MoveState.ExternalForcesVelocity;

        unit.Refs.CC.Move(finalVelocity * dt);
    }
}