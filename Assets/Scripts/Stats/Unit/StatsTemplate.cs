using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsTemplate", menuName = "Stats/StatsTemplate", order = 1)]
public class StatsTemplate : ScriptableObject
{
    [SerializeField] public HealthStats Health;
    [SerializeField] public MovementStats Movement;
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
[Serializable]
public struct HealthStats
{
    public float MaxHealth;
    public float Armor;
    public float Regeneration;
    public float RegenerationDelay;

    public static HealthStats operator +(HealthStats a, HealthStats b)
    {
        return new HealthStats()
        {
            MaxHealth = a.MaxHealth + b.MaxHealth,
            Armor = a.Armor + b.Armor,
            Regeneration = a.Regeneration + b.Regeneration,
            RegenerationDelay = a.RegenerationDelay + b.RegenerationDelay
        };
    }
}