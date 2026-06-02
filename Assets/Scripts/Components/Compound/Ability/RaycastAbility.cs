using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Components/Compound/Ability/Raycast Ability")]
public class RaycastAbility : AbilitySO
{
    public override void Fire(ComponentRuntimeStats statsCarrier, PositionArgs posArgs, Unit sourceUnit)
    {
        if (LaunchComponents.Effect != null)
            LaunchComponents.Effect.Affect(sourceUnit, statsCarrier);

        if (LaunchComponents.PeriodicBehaviour != null)
            LaunchComponents.PeriodicBehaviour.ApplyBehavior(sourceUnit);

        if (LaunchComponents.TemporaryBehaviour != null)
            LaunchComponents.TemporaryBehaviour.ApplyBehavior(sourceUnit);

        if (LaunchComponents.AreaSearcher != null)
            LaunchComponents.AreaSearcher.Search(statsCarrier, posArgs, sourceUnit);
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

        RaycastHit hit = LaunchComponents.Raycaster.Raycast(statsCarrier, posArgs.position, posArgs.direction);
        if (hit.collider != null)
            Debug.DrawLine(posArgs.position, hit.point, Color.red, 0.05f);
        else
            Debug.DrawLine(posArgs.position, posArgs.position + posArgs.direction * statsCarrier.GetStats(LaunchComponents.Raycaster).Range, Color.red, 0.05f);
        if (hit.collider != null)
        {
            hit.collider.TryGetComponent(out Unit hitUnit);
            OnHit(statsCarrier, new PositionArgs(hit.point, posArgs.rotation, posArgs.direction), sourceUnit, hitUnit);
        }
    }
    public override void OnHit(ComponentRuntimeStats statsCarrier, PositionArgs hitPos, Unit sourceUnit, Unit hitUnit)
    {
        if (hitUnit != null)
        {
            if (ImpactComponents.Effect != null)
                ImpactComponents.Effect.Affect(hitUnit, statsCarrier);

            if (ImpactComponents.PeriodicBehaviour != null)
                ImpactComponents.PeriodicBehaviour.ApplyBehavior(hitUnit);

            if (ImpactComponents.TemporaryBehaviour != null)
                ImpactComponents.TemporaryBehaviour.ApplyBehavior(hitUnit);
        }

        if (ImpactComponents.AreaSearcher != null)
            ImpactComponents.AreaSearcher.Search(statsCarrier, hitPos, sourceUnit);

        if (ImpactComponents.Abilities != null)
        {
            for (int j = 0; j < ImpactComponents.Abilities.Count; j++)
            {
                ImpactComponents.Abilities[j].Fire(statsCarrier, new PositionArgs(hitPos.position, hitPos.rotation, hitPos.direction), sourceUnit);
            }
        }

        if (ImpactComponents.UnitSpawner != null)
        {
            Unit spawned = ImpactComponents.UnitSpawner.Spawn(new PositionArgs(hitPos.position, Quaternion.identity), sourceUnit);
            spawned.OnSpawn(sourceUnit);
        }
    }
}
