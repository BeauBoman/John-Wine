using System;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public AbilityStatsTemplate StatsTemplate;
    [Header("Launch")]
    [SerializeField] internal SimulationComponentsPack LaunchComponents;
    [Header("Impact")]
    [SerializeField] internal SimulationComponentsPack ImpactComponents;
    public abstract void Fire(PositionArgs positionArgs, Unit owner = null);
    public abstract void OnHit(PositionArgs hitPos, Unit hitUnit, StatsContext statsContext, Unit owner);
}