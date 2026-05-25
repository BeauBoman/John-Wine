using UnityEngine;

[CreateAssetMenu(fileName = "Rotate Mover", menuName = "Components/Simulation/Mover/Rotate Mover", order = 1)]
public class RotateMover : MoverSO
{
    public override void Move(Unit unit, Vector3 dir, float deltaTime)
    {
        var stats = unit.Stats.GetStats(this);

        unit.State.MoveState.Pitch -= dir.y * stats.MaxSpeed;
        unit.State.MoveState.Pitch = Mathf.Clamp(unit.State.MoveState.Pitch, -90f, 90f);

        unit.State.MoveState.Yaw += dir.x * stats.MaxSpeed;

        unit.transform.localRotation = Quaternion.Euler(unit.State.MoveState.Pitch, unit.State.MoveState.Yaw, 0f);
    }
}
