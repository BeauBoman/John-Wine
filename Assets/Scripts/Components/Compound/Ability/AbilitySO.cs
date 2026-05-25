using System;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    [field: SerializeField] public AbilityStats Stats { get; set; }
    [Header("Launch")]
    [SerializeField] internal SimulationComponentsPack LaunchComponents;
    [Header("Impact")]
    [SerializeField] internal SimulationComponentsPack ImpactComponents;
    public abstract void Fire(PositionArgs positionArgs, Unit owner = null);
    public abstract void OnHit(PositionArgs hitPos, Unit hitUnit, ComponentRuntimeStats statsContext, Unit owner);
}
public class Ability
{
    public AbilitySO config;
    protected ComponentRuntimeStats RuntimeStats;
    protected ComponentRuntimeStats LaunchComponentsStats = new();
    protected ComponentRuntimeStats ImpactComponentsStats = new();
    protected Unit owner;

    public bool CanShoot;

    private float _reloadProgress = 0;
    private ModifiableStats<AbilityStats> _stats;
    public void ReloadProgress(float dt)
    {
        if (_reloadProgress < 1.0f)
        {
            _reloadProgress += dt / _stats.Value.ReloadSpeed;

            if (_reloadProgress > 1.0f)
            {
                _reloadProgress = 1.0f;
                CanShoot = true;
            }
        }
    }
    public ComponentRuntimeStats GetImpactComponentsForProjectile()
    {
        return ImpactComponentsStats;
    }
    public void ResetReloadProgress()
    {
        CanShoot = false;
        _reloadProgress = 0;
    }
    public Ability(AbilitySO c, ComponentRuntimeStats s)
    {
        config = c;
        RuntimeStats = s;
        LaunchComponentsStats.SetComponentsStats(config.LaunchComponents);
        ImpactComponentsStats.SetComponentsStats(config.ImpactComponents);

        _stats = s.GetStatsModifiable(config);
    }
}
[Serializable]
public struct AbilityStats
{
    public float ReloadSpeed;
    public float Range;
    public static AbilityStats operator +(AbilityStats a, AbilityStats b)
    {
        return new AbilityStats()
        {
            ReloadSpeed = a.ReloadSpeed + b.ReloadSpeed,
            Range = a.Range + b.Range,
        };
    }
}