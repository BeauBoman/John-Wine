using UnityEngine;

[CreateAssetMenu(fileName = "Trigger Enter Sensor", menuName = "Components/Simulation/Sensor/Trigger Enter Sensor", order = 1)]
public sealed class TriggerEnterSensor : Sensor
{
    internal sealed override bool IsHitViable(Unit hitUnit, Unit sourceUnit)
    {
        return base.IsHitViable (hitUnit, sourceUnit);
    }
}