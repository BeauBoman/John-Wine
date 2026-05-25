using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Components/Compound/Ability/Default Ability")]
public class ProjectileAbility : AbilitySO
{
    public override void Fire(PositionArgs positionArgs, Unit owner)
    {
        Unit spawnedUnit = LaunchComponents.UnitSpawner.Spawn(positionArgs, owner);
    }
    public override void OnHit(PositionArgs hitPos, Unit hitUnit, ComponentRuntimeStats componentsStats, Unit owner)
    {
        if (hitUnit != null)
            ImpactComponents.Effect.Affect(hitUnit, componentsStats.GetStats(ImpactComponents.Effect));
        List<Unit> units = ImpactComponents.AreaSearcher.Search(componentsStats, hitPos.position, Quaternion.identity, owner);
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] != null)
                ImpactComponents.Effect.Affect(units[i], componentsStats.GetStats(ImpactComponents.Effect));
        }
    }
}