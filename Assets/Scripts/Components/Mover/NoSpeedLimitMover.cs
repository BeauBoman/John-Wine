using UnityEngine;

[CreateAssetMenu(fileName = "NoSpeedLimitMover", menuName = "Components/Movers/NoSpeedLimitMover", order = 1)]
public class NoSpeedLimitMover : Mover
{
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        MovementStats m = unit.Stats.Movement;
        bool hasInput = dir.sqrMagnitude > 0.001f;

        float rate = hasInput ? m.Acceleration : m.Deceleration;

        unit.State.CurrentSpeed = Mathf.MoveTowards(unit.State.CurrentSpeed, float.PositiveInfinity, rate * dt);

        unit.transform.Translate(dir * unit.State.CurrentSpeed * dt);
    }
}
