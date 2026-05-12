using System;
using UnityEngine;

[Serializable]
public class Stats
{
    public MovementStats Movement { get { return _originMovement + MovementBuffs; } }
    public MovementState MoveState = new();
    public HealthStats Health { get { return _originHealth + HealthBuffs; } }
    public HealthState HealthState = new();

    public MovementStats MovementBuffs;
    public HealthStats HealthBuffs;

    private HealthStats _originHealth;
    private MovementStats _originMovement;
    public Stats(StatsTemplate template)
    {
        _originHealth = template.Health;
        _originMovement = template.Movement;
    }
}
[Serializable]
public class MovementState
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
[Serializable]
public class HealthState
{
    public float CurrentHealth;
}