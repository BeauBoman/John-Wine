using UnityEngine;

[CreateAssetMenu(fileName = "Rotate Mover", menuName = "Components/Simulation/Mover/Rotate Mover", order = 1)]
public class RotateMover : MoverSO
{
    public override void Move(Unit unit, Vector3 dir, float deltaTime)
    {
        var stats = unit.Stats.GetStats(this);

        Vector3 flatTarget = new Vector3(dir.x, unit.transform.position.y, dir.z);
        unit.transform.LookAt(flatTarget);
    }
}
