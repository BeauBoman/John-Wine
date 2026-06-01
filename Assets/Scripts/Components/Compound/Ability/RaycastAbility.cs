using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon", menuName = "Components/Compound/Ability/Raycast Ability")]
public class RaycastAbility : AbilitySO
{
    public override void Fire(ComponentRuntimeStats statsCarrier, PositionArgs positionArgs, Unit owner)
    {
        RaycastHit hit = LaunchComponents.Raycaster.Raycast(statsCarrier, positionArgs.position, positionArgs.direction);
        if (hit.collider != null)
            Debug.DrawLine(positionArgs.position, hit.point, Color.red, 0.05f);
        else
            Debug.DrawLine(positionArgs.position, positionArgs.position + positionArgs.direction * statsCarrier.GetStats(LaunchComponents.Raycaster).Range, Color.red, 0.05f);
        if (hit.collider != null)
        {
            hit.collider.TryGetComponent(out Unit hitUnit);
            OnHit(statsCarrier, new PositionArgs(hit.point, Quaternion.identity), owner, hitUnit);
            if (hitUnit != null)
                Debug.Log("BOOM");
        }
    }
    public override void OnHit(ComponentRuntimeStats statsCarrier, PositionArgs hitPos, Unit sourceUnit, Unit hitUnit)
    {
        if (hitUnit != null)
        {
            if (ImpactComponents.Effect != null)
                ImpactComponents.Effect.Affect(hitUnit, statsCarrier.GetStats(ImpactComponents.Effect));
        }
        if (ImpactComponents.AreaSearcher != null)
            ImpactComponents.AreaSearcher.Search(statsCarrier, hitPos.position, Quaternion.identity, sourceUnit);
    }
}
