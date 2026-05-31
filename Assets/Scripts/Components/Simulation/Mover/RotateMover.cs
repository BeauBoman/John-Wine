using UnityEngine;

[CreateAssetMenu(fileName = "Rotate Mover", menuName = "Components/Simulation/Mover/Rotate Mover", order = 1)]
public class RotateMover : MoverSO
{
    public override void Move(Unit unit, Vector3 dir, float deltaTime)
    {
        var stats = unit.Stats.GetStats(this);

        Vector2 input = new Vector2(dir.x, dir.y);
        bool hasInput = input.sqrMagnitude > 0.01f;

        Vector2 targetVelocity = input * stats.MaxSpeed;
        float rate = hasInput ? stats.Acceleration : stats.Deceleration;

        var unitSM = unit.State.MoveState;

        unitSM.RotationalVelocity = Vector2.MoveTowards(unitSM.RotationalVelocity, targetVelocity, rate * deltaTime);

        unitSM.Pitch -= unit.State.MoveState.RotationalVelocity.y * deltaTime;
        unitSM.Pitch = Mathf.Clamp(unitSM.Pitch, -90f, 90f);

        unitSM.Yaw += unitSM.RotationalVelocity.x * deltaTime;

        unit.transform.localRotation = Quaternion.Euler(unitSM.Pitch, unitSM.Yaw, 0f);
    }
}
