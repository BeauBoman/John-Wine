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

        SensorStats _sensorStats = statsCarrier.GetStats(Stats.Components.Sensor);
        AreaSearchStats _areaSearchStats = statsCarrier.GetStats(this);
        int _count = Physics.OverlapSphereNonAlloc(pos, _areaSearchStats.Size.x, _colliderBuffer, _sensorStats.LayerFilter);

        if (_count == 0)
            return _detectedUnitsCache;

        // Filtering detected
        for (int i = 0; i < _count; i++)
        {
            if (_colliderBuffer[i].TryGetComponent(out Unit _foundUnit) == false) continue;

            bool _canBeAdded = true;
            if (Stats.Components.Sensor != null)
            {
                _canBeAdded = Stats.Components.Sensor.IsDetectionViable(statsCarrier, _foundUnit, sourceUnit);
            }

            if (Stats.Components.Raycaster != null)
            {
                if (_canBeAdded == false) continue;

                ModifiableStats<RaycastStats> s = statsCarrier.GetStatsModifiable(Stats.Components.Raycaster);
                s.BuffAdd(new RaycastStats { Range = _areaSearchStats.Size.x });

                Vector3 _middle = _foundUnit.transform.position;
                Vector3[] _targetPoses = new Vector3[5]
                {
                            _middle,
                            _middle + Vector3.up,
                            _middle + Vector3.down,
                            _middle + Vector3.left,
                            _middle + Vector3.right
                };
                for (int j = 0; j < _targetPoses.Length; j++)
                {
                    Vector3 _rayDir = (_targetPoses[j] - pos).normalized;
                    RaycastHit _hit = Stats.Components.Raycaster.Raycast(statsCarrier, pos, _rayDir);
                    if (_hit.collider != null && _hit.collider.transform == _foundUnit.transform)
                    {
                        _detectedUnitsCache.Add(_foundUnit);
                        break;
                    }
                }

            }
            else
            {
                _detectedUnitsCache.Add(_foundUnit);
            }
            _colliderBuffer[i] = null;
        }

        // Applying components for each filtered
        for (int i = 0; i < _detectedUnitsCache.Count; i++)
        {
            Unit _u = _detectedUnitsCache[i];

            if (Stats.Components.Effect != null)
                Stats.Components.Effect.Affect(_u, statsCarrier);

            if (Stats.Components.PeriodicBehaviour != null)
                Stats.Components.PeriodicBehaviour.ApplyBehavior(_u);

            if (Stats.Components.TemporaryBehaviour != null)
                Stats.Components.TemporaryBehaviour.ApplyBehavior(_u);

            if (Stats.Components.AreaSearcher != null)
                Stats.Components.AreaSearcher.Search(statsCarrier, _u.transform.position, Quaternion.identity, sourceUnit);

            if (Stats.Components.Abilities != null)
            {
                for (int j = 0; j < Stats.Components.Abilities.Count; j++)
                {
                    Vector3 direction = (_u.transform.position - pos).normalized;
                    Quaternion rot = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;

                    Stats.Components.Abilities[j].Fire(statsCarrier, new PositionArgs (_u.transform.position, rot, direction), sourceUnit);
                }
            }

            if (Stats.Components.UnitSpawner != null)
                Stats.Components.UnitSpawner.Spawn(new PositionArgs(_u.transform.position, Quaternion.identity), sourceUnit);
        }

        return _detectedUnitsCache;
    }
}