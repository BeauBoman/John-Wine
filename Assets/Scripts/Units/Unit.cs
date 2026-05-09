using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] internal UnitStats Components;
    [SerializeField] internal StatsTemplate StatsTemplate;
    [SerializeField] public Stats Stats;
    [SerializeField] public UnitState State;
    [SerializeField] internal References Refs;
    private void Awake()
    {
        State = new();
        if (StatsTemplate != null)
            Stats = new(StatsTemplate);
    }
}
[Serializable]
public struct References
{
    [SerializeField] public Rigidbody RB;
    [SerializeField] public CharacterController CC;
}
[Flags]
public enum Tags
{
    None = 0,
    Projectile = 1 << 0,
    Invulnerable = 1 << 1,
    Obsticle = 1 << 2
}