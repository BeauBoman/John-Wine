using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere Unit AreaSearcher", menuName = "Components/Compound/AreaSearcher/Sphere Unit AreaSearcher")]
public class DefaultAreaSearch : AreaSearchSO
{
    private readonly Collider[] _colliderBuffer = new Collider[50];
    private readonly List<Unit> _detectedUnitsCache = new List<Unit>(50);
    public override List<Unit> Search(ComponentRuntimeStats statsCarrier, PositionArgs posArgs, Unit sourceUnit)
    {
        _detectedUnitsCache.Clear();

        AreaSearchStats _areaSearchStats = statsCarrier.GetStats(this);
        SensorStats _sensorStats = statsCarrier.GetStats(Stats.Components.Sensor);
        int _count = Physics.OverlapSphereNonAlloc(posArgs.position, _areaSearchStats.Size.x, _colliderBuffer, _sensorStats.LayerFilter);

        if (_count == 0)
            return _detectedUnitsCache;

        // Filtering detected
        for (int i = 0; i < _count; i++)
        {
            if (_colliderBuffer[i].TryGetComponent(out Unit _foundUnit) == false) continue;

            if (Stats.Components.Sensor != null)
            {
                if (Stats.Components.Sensor.IsDetectionViable(statsCarrier, _foundUnit, sourceUnit) == false)
                {
                    _colliderBuffer[i] = null;
                    continue;
                }
            }

            if (Stats.Components.Raycaster != null)
            {
                ModifiableStats<RaycastStats> s = statsCarrier.GetStatsModifiable(Stats.Components.Raycaster);

                float neededLenght = Vector3.Distance(posArgs.position, _foundUnit.transform.position);
                s.BuffAdd(new RaycastStats { Range = neededLenght });

                Vector3 _middle = _foundUnit.transform.position;
                Vector3[] _targetPoses = new Vector3[5]
                {
        _middle,
        _middle + Vector3.up,
        _middle + Vector3.down,
        _middle + Vector3.left,
        _middle + Vector3.right
                };

                bool _hitValidTarget = false;
                Vector3 forward = posArgs.direction.normalized;

                for (int j = 0; j < _targetPoses.Length; j++)
                {
                    Vector3 toTargetOffset = _targetPoses[j] - posArgs.position;
                    float targetDistance = toTargetOffset.magnitude;
                    Vector3 _rayDir = targetDistance > 0.001f ? toTargetOffset / targetDistance : Vector3.zero;
                    RaycastHit _hit = Stats.Components.Raycaster.Raycast(statsCarrier, posArgs.position, _rayDir);

                    bool isClearView = _hit.collider == null ||
                                       _hit.distance >= targetDistance ||
                                       _hit.collider.transform == _foundUnit.transform;

                    if (isClearView)
                    {
                        float _dot = Vector3.Dot(forward, _rayDir);
                        if (_dot > _areaSearchStats.CosCutOff)
                        {
                            _detectedUnitsCache.Add(_foundUnit);
                            _hitValidTarget = true;
                        }
                        break;
                    }
                }
                if (_hitValidTarget == false)
                {
                    _colliderBuffer[i] = null;
                    continue;
                }
                s.BuffAdd(new RaycastStats { Range = -neededLenght });
            }

            _colliderBuffer[i] = null;
        }

        // Applying components for each filtered
        for (int i = 0; i < _detectedUnitsCache.Count; i++)
        {
            Unit _u = _detectedUnitsCache[i];
            Vector3 dir = (_u.transform.position - posArgs.position).normalized;
            Quaternion rot = dir != Vector3.zero ? Quaternion.LookRotation(dir) : Quaternion.identity;

            if (Stats.Components.Effect != null)
                Stats.Components.Effect.Affect(_u, statsCarrier);

            if (Stats.Components.PeriodicBehaviour != null)
                Stats.Components.PeriodicBehaviour.ApplyBehavior(_u);

            if (Stats.Components.TemporaryBehaviour != null)
                Stats.Components.TemporaryBehaviour.ApplyBehavior(_u);

            if (Stats.Components.AreaSearcher != null)
                Stats.Components.AreaSearcher.Search(statsCarrier, new PositionArgs(_u.transform.position, rot, dir), sourceUnit);

            if (Stats.Components.Abilities != null)
            {
                for (int j = 0; j < Stats.Components.Abilities.Count; j++)
                {
                    Stats.Components.Abilities[j].Fire(statsCarrier, new PositionArgs(_u.transform.position, rot, dir), sourceUnit);
                }
            }

            if (Stats.Components.UnitSpawner != null)
                Stats.Components.UnitSpawner.Spawn(new PositionArgs(_u.transform.position, rot, dir), sourceUnit);
        }

        return _detectedUnitsCache;
    }
}