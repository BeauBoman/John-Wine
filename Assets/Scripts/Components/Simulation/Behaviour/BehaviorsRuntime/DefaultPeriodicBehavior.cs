using UnityEngine;

public class DefaultPeriodicBehavior : PeriodicBehavior
{
    public override void OnPeriod()
    {
        if (PeriodicComponents.Effect != null)
            PeriodicComponents.Effect.Affect(target, statsCarrier.GetStats(PeriodicComponents.Effect));
    }
    public DefaultPeriodicBehavior(PeriodicBehaviorSO config, Unit targetUnit, ComponentRuntimeStats statsCarrier, SimulationComponentsPack periodicComponents) : base(config, targetUnit, statsCarrier, periodicComponents) { }
}
