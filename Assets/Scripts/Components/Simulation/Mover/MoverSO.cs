using System;
using UnityEngine;

public abstract class MoverSO : ScriptableObject
{
    [field:SerializeField] public MovementStats Stats { get; private set; }
    public abstract void Move(Unit unit, Vector3 moveDir, float dt);

    protected void UpdateSpeed(MovementStats m, MovementState cm, bool accelerating, float dt)
    {
        float rate = accelerating ? m.Acceleration : m.Deceleration;
        float target = accelerating ? m.MaxSpeed : 0f;

        cm.CurrentSpeed = Mathf.MoveTowards(cm.CurrentSpeed, target, rate * dt);
    }
}
[Serializable]
public struct MovementStats
{
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float Gravity;
    public float JumpForce;

    public static MovementStats operator +(MovementStats a, MovementStats b)
    {
        return new MovementStats
        {
            MaxSpeed = a.MaxSpeed + b.MaxSpeed,
            Acceleration = a.Acceleration + b.Acceleration,
            Deceleration = a.Deceleration + b.Deceleration,
        };
    }
}