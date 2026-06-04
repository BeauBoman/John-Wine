using System;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    [field: SerializeField] public AbilityStats Stats { get; set; }
    [Header("Launch")]
    [SerializeField] internal SimulationComponentsPack LaunchComponents;
    [Header("Impact")]
    [SerializeField] internal SimulationComponentsPack ImpactComponents;
    public abstract void Fire(ComponentRuntimeStats statsCarrier, PositionArgs raycastPos, PositionArgs firePointPos, Unit owner = null);
    public abstract void OnHit(ComponentRuntimeStats statsCarrier, PositionArgs hitPos, Unit sourceUnit, Unit hitUnit);
}
public class Ability
{
    public AbilitySO config;
    protected ComponentRuntimeStats RuntimeStats;
    protected Unit owner;

    public bool CanShoot;

    private float _reloadProgress = 0;
    private ModifiableStats<AbilityStats> _stats;
    public void Fire(PositionArgs raycastPos, PositionArgs firePointPos, Unit whoFired)
    {
        config.Fire(RuntimeStats, raycastPos, firePointPos, whoFired);
    }
    public void OnHit(PositionArgs hitPos, Unit sourceUnit, Unit hitUnit)
    {
        config.OnHit(RuntimeStats, hitPos, sourceUnit, hitUnit);
    }
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
    public void ResetReloadProgress()
    {
        CanShoot = false;
        _reloadProgress = 0;
    }
    public Ability(AbilitySO c, ComponentRuntimeStats s)
    {
        config = c;
        RuntimeStats = s;

        _stats = s.GetStatsModifiable(config);
    }
}
[Serializable]
public struct AbilityStats
{
    public float ReloadSpeed;
}