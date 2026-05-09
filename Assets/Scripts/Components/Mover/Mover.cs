using System;
using UnityEngine;

public abstract class Mover : ScriptableObject
{
    public abstract void Move(Unit unit, Vector3 moveDir, float dt);

    protected void UpdateSpeed(MovementStats stats, UnitState state, bool accelerating,
    float dt)
    {
        float rate = accelerating ? stats.Acceleration : stats.Deceleration;
        float target = accelerating ? stats.MaxSpeed : 0f;

        state.CurrentSpeed = Mathf.MoveTowards(state.CurrentSpeed, target, rate * dt);
    }
}