using System;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public WeaponStatsTemplate StatsTemplate;
    [Header("Launch")]
    [SerializeField] internal SimulationComponentsPack LaunchComponents;
    [Header("Impact")]
    [SerializeField] internal SimulationComponentsPack ImpactComponents;
    public abstract void Fire(PositionArgs positionArgs);
    public abstract void OnHit();
}