using UnityEngine;

[CreateAssetMenu(fileName = "Temporary Buff Behavior", menuName = "Components/Compound/Behavior/Default Temporary Behavior")]
public sealed class DefaultTemporaryBehaviorSO : TemporaryBehaviorSO
{
    public override void ApplyBehavior(Unit targetUnit)
    {
        targetUnit.BehaviorMachine.ApplyBehavior(CreateTempBehavior(targetUnit));
    }
    protected override TemporaryBehavior CreateTempBehavior(Unit targetUnit)
    {
        return new DefaultTemporaryBehavior(this, targetUnit, targetUnit.Stats, StartComponents, EndComponents);
    }
}
