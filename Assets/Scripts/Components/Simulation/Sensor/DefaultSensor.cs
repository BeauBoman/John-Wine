using UnityEngine;

[CreateAssetMenu(fileName = "Default Sensor", menuName = "Components/Simulation/Sensor/Default Sensor", order = 1)]
public sealed class DefaultSensor : Sensor
{
    internal sealed override bool IsHitViable(Unit hitUnit, Unit sourceUnit)
    {
        return base.IsHitViable (hitUnit, sourceUnit);
    }
}