using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public UnitComponent UnitComponent;
    [SerializeField] internal References Refs;
    [SerializeField] public Stats Stats;
    [HideInInspector] public Unit Owner;
    public StatsContext StatsContext = new();

    public event Action OnStartEvent;
    public event Action OnTakeDamage;
    public event Action OnHealthIsZero;

    public void OnStart()
    {
        if (UnitComponent.StatsTemplate != null)
            Stats = new(UnitComponent.StatsTemplate);
        OnStartEvent?.Invoke();

        if(UnitComponent.Ability != null)
        StatsContext.AbilityStats = (new AbilityStats(UnitComponent.Ability.StatsTemplate));
    }

    public void TakeDamage(float amount)
    {
        float finalAmount = amount - Stats.Health.Armor;
        if(finalAmount < 0)
        {
            finalAmount = 0;
            return;
        }
        Stats.HealthState.CurrentHealth -= finalAmount;

        OnTakeDamage?.Invoke();
        if(Stats.HealthState.CurrentHealth <= 0)
        {
            OnHealthIsZero?.Invoke();
        }
    }
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
    Hidden = 1 << 4
}