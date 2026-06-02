using UnityEngine;

[CreateAssetMenu(fileName = "Temporary Buff Behavior", menuName = "Components/Compound/Behavior/Default Periodic Behavior")]
public sealed class DefaultPeriodicBehaviorSO : PeriodicBehaviorSO
{
    public override void ApplyBehavior(Unit targetUnit)
    {
        targetUnit.BehaviorMachine.ApplyBehavior(CreatePeriodBehavior(targetUnit));
    }
    protected override PeriodicBehavior CreatePeriodBehavior(Unit targetUnit)
    {
        return new DefaultPeriodicBehavior(this, targetUnit, PeriodicComponents);
    }
}
