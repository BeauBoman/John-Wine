using UnityEngine;

public sealed class DefaultTemporaryBehavior : TemporaryBehavior
{
    public sealed override void OnStart()
    {
        if (StartComponents.Effect != null)
            StartComponents.Effect.Affect(target, stats.GetStats(StartComponents.Effect));
    }
    public sealed override void OnEnd()
    {
        if (EndComponents.Effect != null)
            EndComponents.Effect.Affect(target, stats.GetStats(EndComponents.Effect));
    }
    public DefaultTemporaryBehavior(TemporaryBehaviorSO config, Unit targetUnit, ComponentRuntimeStats stats, SimulationComponentsPack startComponents, SimulationComponentsPack endComponents) : base(config, targetUnit, stats, startComponents, endComponents) { }
}
