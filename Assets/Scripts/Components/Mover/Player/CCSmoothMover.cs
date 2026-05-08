using UnityEngine;

[CreateAssetMenu(fileName = "CCSmoothMover", menuName = "Components/Movers/CharacterController/CCSmoothMover")]
public class CCSmoothMover : Mover
{
    public override void Move(Unit unit, Vector3 moveDir, float dt)
    {
        MovementStats stats = unit.Stats.Movement;
        UnitState state = unit.State;

        bool hasInput = moveDir.sqrMagnitude > 0.001f;

        if (hasInput)
        {
            state.CurrentMoveDirection = moveDir.normalized;
        }

        UpdateSpeed(stats, state, hasInput, dt);
        Vector3 horizontalVelocity = state.CurrentMoveDirection * state.CurrentSpeed;
        Vector3 finalVelocity = horizontalVelocity + state.ExternalForcesVelocity;

        unit.Refs.CC.Move(finalVelocity * dt);
    }
}