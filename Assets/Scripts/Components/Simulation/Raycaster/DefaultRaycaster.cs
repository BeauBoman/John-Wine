using UnityEngine;

[CreateAssetMenu(fileName ="Default Raycaster", menuName ="Components/Simulation/Raycast/Default Raycaster")]
public class DefaultRaycaster : Raycaster
{
    public override RaycastHit Raycast(Vector3 origin, Vector3 dir, float maxDistance)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, maxDistance, _layer))
        {
            return hit;
        }
        return default;
    }
}
