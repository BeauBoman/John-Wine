using System;
using UnityEngine;

public abstract class EffectSO : ScriptableObject
{
    /// <summary>
    /// Returns affected unit.
    /// </summary>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    [field: SerializeField] public EffectStats Stats { get; private set; }
    public abstract Unit Affect(Unit targetUnit, EffectStats stats);
}
[Serializable]
public struct EffectStats
{
    public float Amount;

    public static EffectStats operator +(EffectStats a, EffectStats b)
    {
        return new EffectStats
        {
            Amount = a.Amount + b.Amount,
        };
    }
}

