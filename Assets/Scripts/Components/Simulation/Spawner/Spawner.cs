using System;
using UnityEngine;

public abstract class Spawner<T> : ScriptableObject
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Vector3 PositionOffset;
    [SerializeField] protected Quaternion RotationOffset;
    public abstract T Spawn(PositionArgs args);
}
public struct PositionArgs
{
    public Vector3 position;
    public Quaternion rotation;

    public PositionArgs(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}