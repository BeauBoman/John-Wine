using System;
using UnityEngine;

public class Sensor : ScriptableObject
{
    [SerializeField] protected Tags CollisionFilter;
    [SerializeField] protected bool CollideWithOwner;

    internal virtual bool IsHitViable(Unit hitUnit, Unit sourceUnit)
    {
        if (hitUnit == null) return false;

        if (CollideWithOwner == false && sourceUnit.Owner == hitUnit) return false;
        if ((hitUnit.UnitComponent.Tags & CollisionFilter) == 0) return false;

        return true;
    }
}