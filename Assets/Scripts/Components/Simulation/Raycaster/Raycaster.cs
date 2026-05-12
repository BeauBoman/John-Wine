using UnityEngine;

public abstract class Raycaster : ScriptableObject
{
    [SerializeField] protected LayerMask _layer;
    public abstract RaycastHit Raycast(Vector3 origin, Vector3 dir, float maxDistance);
}
