using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Spawn Ability", menuName = "Components/Compound/Ability/Default Spawn Ability")]
public class SpawnAbility : AbilitySO
{
    public override void Fire(ComponentRuntimeStats statsCarrier, PositionArgs raycastPos, PositionArgs firePointPos, Unit sourceUnit)
    {
        Debug.Log("Fire!");
        if (LaunchComponents.Effect != null)
            LaunchComponents.Effect.Affect(sourceUnit, statsCarrier);

        if (LaunchComponents.PeriodicBehaviour != null)
            LaunchComponents.PeriodicBehaviour.ApplyBehavior(sourceUnit);

        if (LaunchComponents.TemporaryBehaviour != null)
            LaunchComponents.TemporaryBehaviour.ApplyBehavior(sourceUnit);

        if (LaunchComponents.AreaSearcher != null)
            LaunchComponents.AreaSearcher.Search(statsCarrier, raycastPos, sourceUnit);

        if (LaunchComponents.Raycaster != null)
        {
            RaycastHit _hit = LaunchComponents.Raycaster.Raycast(statsCarrier, raycastPos.position, raycastPos.direction);

            if (_hit.point != default && _hit.distance > 1)
            {
                Vector3 _desiredFireDirection = (_hit.point - firePointPos.position).normalized;

                firePointPos = new PositionArgs(firePointPos.position, Quaternion.LookRotation(_desiredFireDirection), firePointPos.direction);
            }
        }

        if (LaunchComponents.Abilities != null)
        {
            for (int j = 0; j < LaunchComponents.Abilities.Count; j++)
            {
                LaunchComponents.Abilities[j].Fire(statsCarrier, raycastPos, firePointPos, sourceUnit);
            }
        }

        if (LaunchComponents.UnitSpawner != null)
        {
            Unit spawned = LaunchComponents.UnitSpawner.Spawn(firePointPos, sourceUnit);
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