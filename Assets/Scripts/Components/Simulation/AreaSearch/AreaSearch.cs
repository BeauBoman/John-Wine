using System.Collections.Generic;
using UnityEngine;

public abstract class AreaSearch : ScriptableObject
{
    [SerializeField] protected CollisionFilter _filter;
    [SerializeField] protected LayerMask _layer;
    public abstract List<Unit> Search(Vector3 size, Vector3 pos, Quaternion rotation, Unit owner);
    protected bool IsDetectionViable(Unit hitUnit, Unit sourceUnit)
    {
        if (hitUnit == null) return false;

        if (_filter.DetectOwner == false && sourceUnit.Owner == hitUnit) return false;
        if ((hitUnit.UnitComponent.Tags & _filter.TagFilter) == 0) return false;

        return true;
    }
}
public struct AreaSearchResult
{
    public Unit[] Units;
    public Avatar[] Avatars;
}
