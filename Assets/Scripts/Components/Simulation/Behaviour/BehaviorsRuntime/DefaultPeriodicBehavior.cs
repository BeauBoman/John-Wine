using UnityEngine;

public class DefaultPeriodicBehavior : PeriodicBehavior
{
    public override void OnPeriod()
    {
        if (PeriodicComponents.Effect != null)
            PeriodicComponents.Effect.Affect(target, statsCarrier);
    }
    public DefaultPeriodicBehavior(PeriodicBehaviorSO config, Unit targetUnit, SimulationComponentsPack periodicComponents) : base(config, targetUnit, periodicComponents) { }
}
