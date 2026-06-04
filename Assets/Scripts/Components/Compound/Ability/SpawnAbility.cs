using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Spawn Ability", menuName = "Components/Compound/Ability/Default Spawn Ability")]
public class SpawnAbility : AbilitySO
{
    public override void Fire(ComponentRuntimeStats statsCarrier, PositionArgs posArgs, Unit sourceUnit)
    {
        Debug.Log("Fire!");
        if (LaunchComponents.Effect != null)
            LaunchComponents.Effect.Affect(sourceUnit, statsCarrier);

        if (LaunchComponents.PeriodicBehaviour != null)
            LaunchComponents.PeriodicBehaviour.ApplyBehavior(sourceUnit);

        if (LaunchComponents.TemporaryBehaviour != null)
            LaunchComponents.TemporaryBehaviour.ApplyBehavior(sourceUnit);

        if (LaunchComponents.AreaSearcher != null)
            LaunchComponents.AreaSearcher.Search(statsCarrier, posArgs, sourceUnit);

        if (LaunchComponents.Abilities != null)
        {
            for (int j = 0; j < LaunchComponents.Abilities.Count; j++)
            {
                LaunchComponents.Abilities[j].Fire(statsCarrier, posArgs, sourceUnit);
            }
        }

        if (LaunchComponents.UnitSpawner != null)
        {
            Unit spawned = LaunchComponents.UnitSpawner.Spawn(posArgs, sourceUnit);
            if (spawned != null && spawned.ControllerScript is IAbilityConfigCarrier abilityCarrier)
                abilityCarrier.abilitySO = this;
            spawned.OnSpawn(sourceUnit);
        }
    }
    public override void OnHit(ComponentRuntimeStats statsCarrier, PositionArgs hitPos, Unit sourceUnit, Unit hitUnit)
    {
        if (hitUnit != null)
        {
            if (ImpactComponents.Effect != null)
                ImpactComponents.Effect.Affect(hitUnit, statsCarrier);
        }
        ImpactComponents.AreaSearcher.Search(statsCarrier, hitPos, sourceUnit);
    }
}