using System;
using UnityEngine;

public abstract class Mover : ScriptableObject
{
    public abstract void Move(Unit unit, Vector3 moveDir, float dt);

    protected void UpdateSpeed(MovementStats m, MovementState cm, bool accelerating, float dt)
    {
        float rate = accelerating ? m.Acceleration : m.Deceleration;
        float target = accelerating ? m.MaxSpeed : 0f;

        cm.CurrentSpeed = Mathf.MoveTowards(cm.CurrentSpeed, target, rate * dt);
    }
}