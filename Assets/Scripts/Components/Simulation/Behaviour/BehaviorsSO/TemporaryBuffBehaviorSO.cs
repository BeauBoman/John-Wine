using UnityEngine;

[CreateAssetMenu(fileName = "Temporary Buff Behavior", menuName = "Components/Simulation/Behavior/Temporary Buff Behavior")]
public sealed class TemporaryBuffBehaviorSO : TemporaryBehaviorSO
{
    protected sealed override TemporaryBehavior CreateTempBehavior(Unit targetUnit)
    {
        return new DefaultTemporaryBehavior(this, targetUnit, targetUnit.Stats, StartComponents, EndComponents);
    }
    public sealed override void ApplyBehavior(Unit targetUnit)
    {
        targetUnit.BehaviorMachine.ApplyBehavior(CreateTempBehavior(targetUnit));
    }
}
