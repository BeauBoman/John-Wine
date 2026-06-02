using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaSearchSO : ScriptableObject
{
    [field: SerializeField] public AreaSearchStats Stats { get; private set; }
    /// <summary>
    /// Method searches for units in area and calls components for each one of them.
    /// </summary>
    /// <returns>List of found objects</returns>
    public abstract List<Unit> Search(ComponentRuntimeStats statsCarrier, Vector3 pos, Quaternion rotation, Unit owner);
}
[Serializable]
public struct AreaSearchStats
{
    public Vector3 Size;

    public SimulationComponentsPack Components;
}