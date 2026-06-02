using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "Components/Simulation/Effect/Damage Effect")]
public class DealDamageEffect : EffectSO
{
    public override Unit Affect(Unit targetUnit, ComponentRuntimeStats statsCarrier)
    {
        if(targetUnit == null) return null;

        targetUnit.TakeDamage(statsCarrier.GetStats(this).Amount);
        return targetUnit;
    }
}
