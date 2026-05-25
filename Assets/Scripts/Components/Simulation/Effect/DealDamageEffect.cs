using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "Components/Simulation/Effect/Damage Effect")]
public class DealDamageEffect : EffectSO
{
    public override Unit Affect(Unit targetUnit, EffectStats stats)
    {
        if(targetUnit == null) return null;

        targetUnit.TakeDamage(stats.Amount);
        return targetUnit;
    }
}
