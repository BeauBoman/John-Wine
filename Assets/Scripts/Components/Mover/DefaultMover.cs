using UnityEngine;

[CreateAssetMenu(fileName = "DefaultMover", menuName = "Components/Movers/DefaultMover", order = 1)]
public class DefaultMover : Mover
{
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        MovementStats m = unit.Stats.Movement;
        bool moving = dir.sqrMagnitude > 0.001f;
        UpdateSpeed(m, unit.State, moving, dt);

        unit.transform.Translate(dt * unit.State.CurrentSpeed * dir);
    }
}