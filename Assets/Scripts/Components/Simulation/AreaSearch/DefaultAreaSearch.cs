using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sphere AreaSearcher", menuName ="Components/Simulation/AreaSearcher/Sphere AreaSearcher")]
public class DefaultAreaSearch : AreaSearch
{
    private readonly Collider[] _colliderBuffer = new Collider[50];
    public override List<Unit> Search(Vector3 size, Vector3 pos, Quaternion rotation)
    {
        int count = Physics.OverlapSphereNonAlloc(pos, size.x, _colliderBuffer, _layer);

        if (count == 0)
            return null;

        List<Unit> detectedUnits = new();
        for (int i = 0; i < count; i++)
        {
            if (_colliderBuffer[i] == null) break;
            if (_colliderBuffer[i].TryGetComponent(out Unit unit))
            {
                detectedUnits.Add(unit);
            }
            _colliderBuffer[i] = null;
        }
        return detectedUnits;
    }
}
