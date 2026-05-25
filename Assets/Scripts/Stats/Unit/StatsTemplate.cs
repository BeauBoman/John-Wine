using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsTemplate", menuName = "Stats/StatsTemplate", order = 1)]
public class StatsTemplate : ScriptableObject
{
    [SerializeField] public HealthStats Health;
}
[Serializable]
public struct HealthStats
{
    public float MaxHealth;
    public float HealthOnStart;
    public float Armor;
    public float Regeneration;
    public float RegenerationDelay;
}