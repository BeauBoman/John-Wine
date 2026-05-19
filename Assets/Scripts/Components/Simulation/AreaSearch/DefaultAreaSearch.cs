using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere AreaSearcher", menuName = "Components/Simulation/AreaSearcher/Sphere AreaSearcher")]
public class DefaultAreaSearch : AreaSearch
{
    private readonly Collider[] _colliderBuffer = new Collider[50];
    private readonly List<Unit> _detectedUnitsCache = new List<Unit>(50);

    public override List<Unit> Search(Vector3 size, Vector3 pos, Quaternion rotation, Unit owner)
    {
        _detectedUnitsCache.Clear();

        Debug.DrawLine(pos, pos + Vector3.up * size.x, Color.red, 2f);
        int count = Physics.OverlapSphereNonAlloc(pos, size.x, _colliderBuffer, _layer);

        if (count == 0)
            return _detectedUnitsCache;

        for (int i = 0; i < count; i++)
        {
            if (_colliderBuffer[i].TryGetComponent(out Unit unit))
            {
                if (IsDetectionViable(unit, owner))
                    _detectedUnitsCache.Add(unit);
            }
            _colliderBuffer[i] = null;
        }
        Debug.Log(_detectedUnitsCache.Count);
        return _detectedUnitsCache;
    }
}