using UnityEngine;

public sealed class DefaultTemporaryBehavior : TemporaryBehavior
{
    public sealed override void OnStart()
    {
        if (StartComponents.Effect != null)
            StartComponents.Effect.Affect(target, statsCarrier);
    }
    public sealed override void OnEnd()
    {
        if (EndComponents.Effect != null)
            EndComponents.Effect.Affect(target, statsCarrier);
    }
    public DefaultTemporaryBehavior(TemporaryBehaviorSO config, Unit targetUnit, SimulationComponentsPack startComponents, SimulationComponentsPack endComponents) : base(config, targetUnit, startComponents, endComponents) { }
}
