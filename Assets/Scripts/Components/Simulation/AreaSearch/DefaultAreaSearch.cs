using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere Unit AreaSearcher", menuName = "Components/Compound/AreaSearcher/Sphere Unit AreaSearcher")]
public class DefaultAreaSearch : AreaSearchSO
{
    private readonly Collider[] _colliderBuffer = new Collider[50];
    private readonly List<Unit> _detectedUnitsCache = new List<Unit>(50);
    public override List<Unit> Search(ComponentRuntimeStats statsCarrier, Vector3 pos, Quaternion rotation, Unit sourceUnit)
    {
        _detectedUnitsCache.Clear();

        SensorStats sensorStats = statsCarrier.GetStats(Stats.Components.Sensor);
        AreaSearchStats areaSearchStats = statsCarrier.GetStats(this);
        int count = Physics.OverlapSphereNonAlloc(pos, areaSearchStats.Size.x, _colliderBuffer, sensorStats.LayerFilter);

        if (count == 0)
            return _detectedUnitsCache;

        // Filtering detected
        for (int i = 0; i < count; i++)
        {
            if (_colliderBuffer[i].TryGetComponent(out Unit hitUnit))
            {
                bool canBeAdded = true;
                if (Stats.Components.Sensor != null)
                {
                    canBeAdded = Stats.Components.Sensor.IsDetectionViable(statsCarrier, hitUnit, sourceUnit);
                }

                if (Stats.Components.Raycaster != null)
                {
                    if (canBeAdded)
                    {
                        ModifiableStats<RaycastStats> s = statsCarrier.GetStatsModifiable(Stats.Components.Raycaster);
                        s.BuffAdd(new RaycastStats { Range = areaSearchStats.Size.x });
                        RaycastHit hit = Stats.Components.Raycaster.Raycast(statsCarrier, pos, hitUnit.transform.position - pos);
                        if (hit.collider != null)
                            _detectedUnitsCache.Add(hitUnit);
                    }
                }
                else
                {
                    _detectedUnitsCache.Add(hitUnit);
                }

            }
            _colliderBuffer[i] = null;
        }

        // Applying components for each filtered
        if (Stats.Components.Effect != null)
        {
            for (int i = 0; i < _detectedUnitsCache.Count; i++)
            {
                Unit u = _detectedUnitsCache[i];

                Stats.Components.Effect.Affect(u, statsCarrier);
            }
        }
        if (Stats.Components.PeriodicBehaviour != null)
        {
            for (int i = 0; i < _detectedUnitsCache.Count; i++)
            {
                Unit u = _detectedUnitsCache[i];

                Stats.Components.PeriodicBehaviour.ApplyBehavior(u);
            }
        }
        if (Stats.Components.TemporaryBehaviour != null)
        {
            for (int i = 0; i < _detectedUnitsCache.Count; i++)
            {
                Unit u = _detectedUnitsCache[i];

                Stats.Components.TemporaryBehaviour.ApplyBehavior(u);
            }
        }
        return _detectedUnitsCache;
    }
}