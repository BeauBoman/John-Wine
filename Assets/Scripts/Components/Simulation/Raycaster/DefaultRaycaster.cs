using UnityEngine;

[CreateAssetMenu(fileName ="Default Raycaster", menuName ="Components/Simulation/Raycast/Default Raycaster")]
public class DefaultRaycaster : RaycasterSO
{
    public override RaycastHit Raycast(ComponentRuntimeStats statsCarrier, Vector3 origin, Vector3 dir)
    {
        RaycastStats stats = statsCarrier.GetStats(this);
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, stats.Range, stats.Layer))
        {
            return hit;
        }
        return default;
    }
}
