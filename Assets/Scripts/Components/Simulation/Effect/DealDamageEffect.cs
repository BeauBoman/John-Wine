using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "Components/Simulation/Effect/Damage Effect")]
public class DealDamageEffect : Effect
{
    public override Unit Affect(Unit targetUnit)
    {
        if(targetUnit == null) return null;

        targetUnit.TakeDamage(_totalAmount);
        return targetUnit;
    }
}
