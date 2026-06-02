using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Components/Compound/Ability/Default Ability")]
public class SpawnAbility : AbilitySO
{
    public override void Fire(ComponentRuntimeStats statsCarrier, PositionArgs positionArgs, Unit owner)
    {
        Unit spawnedUnit = LaunchComponents.UnitSpawner.Spawn(positionArgs, owner);
    }
    public override void OnHit(ComponentRuntimeStats statsCarrier, PositionArgs hitPos, Unit sourceUnit, Unit hitUnit)
    {
        if (hitUnit != null)
        {
            if (ImpactComponents.Effect != null)
                ImpactComponents.Effect.Affect(hitUnit, statsCarrier);
        }
        ImpactComponents.AreaSearcher.Search(statsCarrier, hitPos.position, Quaternion.identity, sourceUnit);
    }
}