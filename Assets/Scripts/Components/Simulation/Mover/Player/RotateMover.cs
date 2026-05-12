using UnityEngine;

[CreateAssetMenu(fileName = "Rotate Mover", menuName = "Components/Simulation/Mover/Rotate Mover", order = 1)]
public class RotateMover : Mover
{
    public override void Move(Unit unit, Vector3 dir, float deltaTime)
    {
        var stats = unit.Stats.Movement;

        unit.Stats.MoveState.Pitch -= dir.y * stats.MaxSpeed;
        unit.Stats.MoveState.Pitch = Mathf.Clamp(unit.Stats.MoveState.Pitch, -90f, 90f);

        unit.Stats.MoveState.Yaw += dir.x * stats.MaxSpeed;

        unit.transform.localRotation = Quaternion.Euler(unit.Stats.MoveState.Pitch, unit.Stats.MoveState.Yaw, 0f);
    }
}
