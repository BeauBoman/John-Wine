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
    public abstract Unit Affect(Unit targetUnit, ComponentRuntimeStats statsCarrier);
}
[Serializable]
public struct EffectStats
{
    public float Amount;
}

