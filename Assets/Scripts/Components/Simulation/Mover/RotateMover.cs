using UnityEngine;

[CreateAssetMenu(fileName = "Rotate Mover", menuName = "Components/Simulation/Mover/Rotate Mover", order = 1)]
public class RotateMover : MoverSO
{
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        var stats = unit.Stats.GetStats(this);

        Vector3 dirTarget = new Vector3(dir.x, unit.transform.position.y, dir.z);

        //bool accelerating = dirTarget.sqrMagnitude > 0.001;
        //UpdateSpeed(unit.Stats.GetStats(this), unit.State.MoveState, accelerating, dt);

        unit.transform.LookAt(dirTarget);
    }
}
