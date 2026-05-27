using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaSearchSO : ScriptableObject
{
    [field: SerializeField] public AreaSearchStats Stats { get; private set; }
    public abstract List<Unit> Search(ComponentRuntimeStats stats, Vector3 pos, Quaternion rotation, Unit owner);
}
public struct AreaSearchResult
{
    public Unit[] Units;
    public Avatar[] Avatars;
}
[Serializable]
public struct AreaSearchStats
{
    public Vector3 Size;

    public SimulationComponentsPack Components;
}