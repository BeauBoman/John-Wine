using System;
using UnityEngine;

[Serializable]
public sealed class BoomerangAbility : Ability
{
    public override void Fire(PositionArgs raycastPos, PositionArgs firePointPos, Unit whoFired)
    {
        if (IsBlocked == true) return;

        base.Fire(raycastPos, firePointPos, whoFired);
        IsBlocked = true;
    }
    public override void Hold(PositionArgs raycastPos, PositionArgs firePointPos, float dt)
    {
        if (Spawned.Count == 0) return;

        RaycastHit hit = config.LaunchComponents.Raycaster.Raycast(RuntimeStats, raycastPos.position, raycastPos.direction);
        Vector3 dir = Vector3.zero;
        for (int i = 0; i < Spawned.Count; i++)
        {
            if (hit.collider != null)
            {
                dir = hit.point - Spawned[i].transform.position;
            }
            else
            {
                dir = raycastPos.position + (raycastPos.direction * RuntimeStats.GetStats(config.LaunchComponents.Raycaster).Range);
            }
            Spawned[i].UnitSO.SimComponents.Movers.Mover.Move(Spawned[i], dir, dt);
        }
    }
    public override void Release()
    {
        IsBlocked = false;
    }
    public BoomerangAbility(AbilitySO so, ComponentRuntimeStats statsCarrier) : base(so, statsCarrier) { }
}
