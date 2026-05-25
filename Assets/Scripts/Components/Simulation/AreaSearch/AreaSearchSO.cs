using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaSearchSO : ScriptableObject
{
    [field: SerializeField] public AreaSearchStats Stats { get; private set; }
    public abstract List<Unit> Search(ComponentRuntimeStats stats, Vector3 pos, Quaternion rotation, Unit owner);
    protected bool IsDetectionViable(Unit hitUnit, Unit sourceUnit)
    {
        if (hitUnit == null) return false;

        if (Stats.Filter.DetectOwner == false && sourceUnit.Owner == hitUnit) return false;
        if ((hitUnit.UnitSO.Tags & Stats.Filter.TagFilter) == 0) return false;

        return true;
    }
}
public struct AreaSearchResult
{
    public Unit[] Units;
    public Avatar[] Avatars;
}
[Serializable]
public struct AreaSearchStats
{
    public Vector3 Size;
    public CollisionFilter Filter;
    public LayerMask Layer;
    public static AreaSearchStats operator +(AreaSearchStats a, AreaSearchStats b)
    {
        return new AreaSearchStats
        {
            Size = a.Size + b.Size
        };
    }
}