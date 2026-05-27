using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere AreaSearcher", menuName = "Components/Compound/AreaSearcher/Sphere AreaSearcher")]
public class DefaultAreaSearch : AreaSearchSO
{
    private readonly Collider[] _colliderBuffer = new Collider[50];
    private readonly List<Unit> _detectedUnitsCache = new List<Unit>(50);

    public override List<Unit> Search(ComponentRuntimeStats statsCarrier, Vector3 pos, Quaternion rotation, Unit sourceUnit)
    {
        _detectedUnitsCache.Clear();

        SensorStats sensorStats = statsCarrier.GetStats(Stats.Components.Sensor);
        int count = Physics.OverlapSphereNonAlloc(pos, statsCarrier.GetStats(this).Size.x, _colliderBuffer, sensorStats.LayerFilter);

        if (count == 0)
            return _detectedUnitsCache;

        for (int i = 0; i < count; i++)
        {
            if (_colliderBuffer[i].TryGetComponent(out Unit hitUnit))
            {
                if (Stats.Components.Sensor.IsDetectionViable(statsCarrier, hitUnit, sourceUnit))
                    _detectedUnitsCache.Add(hitUnit);
            }
            _colliderBuffer[i] = null;
        }


        EffectStats efStats = statsCarrier.GetStats(Stats.Components.Effect);
        for(int i = 0;i < _detectedUnitsCache.Count; i++)
        {
            Unit u = _detectedUnitsCache[i];

            Stats.Components.Effect.Affect(u, efStats);
        }
        Debug.Log(_detectedUnitsCache.Count);
        return _detectedUnitsCache;
    }
}