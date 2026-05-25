using System;
using UnityEngine;

public class Sensor : ScriptableObject
{
    [SerializeField] private CollisionFilter _filter;
    internal virtual bool IsHitViable(Unit hitUnit, Unit sourceUnit)
    {
        if (hitUnit == null) return false;

        if (_filter.DetectOwner == false && sourceUnit.Owner == hitUnit) return false;
        if ((hitUnit.UnitSO.Tags & _filter.TagFilter) == 0) return false;

        return true;
    }
}
[Serializable]
public struct CollisionFilter
{
    public Tags TagFilter;
    public bool DetectOwner;
}