using System;
using UnityEngine;

public abstract class Spawner<T> : ScriptableObject
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Vector3 PositionOffset;
    public abstract T Spawn(PositionArgs args, T owner = default);
}
public struct PositionArgs
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 direction;
    public Vector3 hitNormal;

    public PositionArgs(Vector3 position, Quaternion rotation, Vector3 direction = default, Vector3 hitNormal = default)
    {
        this.position = position;
        this.rotation = rotation;
        this.direction = direction;
        this.hitNormal = hitNormal;
    }
}