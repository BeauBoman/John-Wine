using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public UnitSO UnitSO;
    [SerializeField] private Controller ControllerScript;
    [SerializeField] internal References Refs;
    [HideInInspector] public Unit Owner;
    [HideInInspector] public BehaviorMachine BehaviorMachine;
    public ComponentRuntimeStats Stats = new();
    public ModifiableStats<HealthStats> Health;
    public UnitState State = new();

    public event Action OnTakeDamageEvent;
    public event Action OnHealthIsZero;

    public void OnSpawn(Unit owner = null)
    {
        Owner = owner;
        OnStart();
    }
    private void OnStart()
    {
        if (UnitSO.StatsTemplate != null)
            Health = new(UnitSO.StatsTemplate.Health);
        BehaviorMachine = new BehaviorMachine(this);

        Stats.SetComponentsStats(UnitSO.SimComponents);
        if (UnitSO.SimComponents.Ability != null)
            State.CurrentAbility = new Ability(UnitSO.SimComponents.Ability, Stats);

        State.HealthState.CurrentHealth = Health.Value.HealthOnStart;

        if (ControllerScript != null)
            ControllerScript.OnStart();
        //OnStartEvent?.Invoke();
    }
    public void TakeDamage(float amount)
    {
        float finalAmount = amount - Health.Value.Armor;
        if (finalAmount < 0)
        {
            finalAmount = 0;
            return;
        }
        State.HealthState.CurrentHealth -= finalAmount;

        OnTakeDamageEvent?.Invoke();
        if (State.HealthState.CurrentHealth <= 0)
        {
            OnHealthIsZero?.Invoke();
        }
    }
}
public class UnitState
{
    public Ability CurrentAbility;

    public HealthState HealthState = new();
    public MovementState MoveState = new();
}
[Serializable]
public struct References
{
    [SerializeField] public Rigidbody RB;
    [SerializeField] public CharacterController CC;
}
[Serializable]
[Flags]
public enum Tags
{
    None = 0,
    Projectile = 1 << 0,
    Entity = 1 << 1,
    Invulnerable = 1 << 2,
    Obsticle = 1 << 3,
    Hidden = 1 << 4,
    Key = 1 << 5
}
public abstract class Controller : MonoBehaviour
{
    public abstract void OnStart();
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
    public Vector3 MovementVelocity;
    public Vector2 RotationalVelocity;
}
[Serializable]
public class HealthState
{
    public float CurrentHealth;
}