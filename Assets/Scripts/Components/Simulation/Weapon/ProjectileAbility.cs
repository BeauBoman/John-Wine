using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Components/Compound/Ability/Default Ability")]
public class ProjectileAbility : Ability
{
    public override void Fire(PositionArgs positionArgs, Unit owner)
    {
        Unit spawnedUnit = LaunchComponents.UnitSpawner.Spawn(positionArgs, owner);
    }
    public override void OnHit(PositionArgs hitPos, Unit hitUnit, StatsContext statsContext, Unit owner)
    {
        ImpactComponents.Effect.Affect(hitUnit);
        List<Unit> units = ImpactComponents.AreaSearcher.Search(statsContext.AbilityStats.Stats.ExplosionSize, hitPos.position, Quaternion.identity, owner);
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] != null)
                ImpactComponents.Effect.Affect(units[i]);
        }
    }
}
