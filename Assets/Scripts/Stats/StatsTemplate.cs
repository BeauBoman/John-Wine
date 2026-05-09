using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsTemplate", menuName = "Stats/StateTemplate", order = 1)]
public class StatsTemplate : ScriptableObject
{
    [SerializeField] public MovementStats Movement;
}
[Serializable]
public struct WeaponStats
{
    public float ShootingSpeed;
    public float Range;
}
[Serializable]
public struct MovementStats
{
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float JumpForce;
    public float Gravity;

    public static MovementStats operator +(MovementStats a, MovementStats b)
    {
        return new MovementStats
        {
            MaxSpeed = a.MaxSpeed + b.MaxSpeed,
            Acceleration = a.Acceleration + b.Acceleration,
            Deceleration = a.Deceleration + b.Deceleration,
            JumpForce = a.JumpForce + b.JumpForce,
            Gravity = a.Gravity + b.Gravity
        };
    }
}

