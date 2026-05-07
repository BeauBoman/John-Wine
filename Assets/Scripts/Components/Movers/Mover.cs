using System;
using UnityEngine;

public abstract class Mover : ScriptableObject, IMover
{
    public float MaxSpeed = 1;
    public float Acceleration = 99999;
    public abstract void Move(Transform obj, Vector3 dir);
}
public interface IMover
{
    public void Move(Transform obj, Vector3 dir);
}