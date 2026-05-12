using UnityEngine;

[CreateAssetMenu(fileName = "Composite Effect", menuName = "Components/Simulation/Effect/Composite Effect")]
public class EffectComposite : Effect
{
    [SerializeField] private Effect[] Effects;
    public override Unit Execute(Unit targetUnit)
    {
        for(int i = 0; i < Effects.Length; i++)
        {
            Effects[i].Execute(targetUnit);
        }
        return targetUnit;
    }
}
