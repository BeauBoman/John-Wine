using System.Collections.Generic;
using UnityEngine;

public abstract class AreaSearch : ScriptableObject
{
    [SerializeField] protected LayerMask _layer;
    public abstract List<Unit> Search(Vector3 size, Vector3 pos, Quaternion rotation);
}
public struct AreaSearchResult
{
    public Unit[] Units;
    public Avatar[] Avatars;
}
