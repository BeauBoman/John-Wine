using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] internal DefaultUnit UnitStats;
    [SerializeField] internal StatsTemplate StatsTemplate;
    [SerializeField] public Stats Stats;
    [SerializeField] public WeaponStats WeaponStats;
    [SerializeField] internal References Refs;

    public event Action OnTakeDamage;
    private void Awake()
    {
        if (StatsTemplate != null)
            Stats = new(StatsTemplate);
        if (UnitStats.Weapon != null)
            WeaponStats = new(UnitStats.Weapon.StatsTemplate);
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
    Invulnerable = 1 << 1,
    Obsticle = 1 << 2,
    Hidden = 1 << 3
}