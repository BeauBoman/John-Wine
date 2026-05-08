using System;
using UnityEngine;

[Serializable]
public class UnitState
{
    public float CurrentSpeed;
    public float CurrentAcceleration;
    public float CurrentDeceleration;
    public Vector3 ExternalForcesVelocity;
    public Vector3 CurrentMoveDirection;

    public float Pitch;
    public float Yaw;
    public float Roll;
}
