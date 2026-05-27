using System;
using UnityEngine;

public class SensorSO : ScriptableObject
{
    [field: SerializeField] public SensorStats Stats { get; private set; }
    internal virtual bool IsDetectionViable(ComponentRuntimeStats statsCarrier, Unit hitUnit, Unit sourceUnit) {  return false; }
}
[Serializable]
public struct SensorStats
{
    public Tags TagFilter;
    public LayerMask LayerFilter;
    public bool DetectOwner;
}