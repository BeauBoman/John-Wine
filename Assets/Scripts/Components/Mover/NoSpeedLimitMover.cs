using UnityEngine;

[CreateAssetMenu(fileName = "NoSpeedLimitMover", menuName = "Components/Movers/NoSpeedLimitMover", order = 1)]
public class NoSpeedLimitMover : Mover
{
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        MovementStats m = unit.Stats.Movement;
        if (dir.magnitude > 0)
            unit.State.CurrentSpeed = Mathf.MoveTowards(unit.State.CurrentSpeed, m.MaxSpeed, m.Acceleration * dt);
        else
            unit.State.CurrentSpeed = Mathf.MoveTowards(unit.State.CurrentSpeed, 0, m.Deceleration * dt);

        unit.transform.Translate(dir * unit.State.CurrentSpeed * dt);
    }
}
