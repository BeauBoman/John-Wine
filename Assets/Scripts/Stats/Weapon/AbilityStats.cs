using System;

public class AbilityStats : IUpdatable
{
    public ShootingStats StatsBuffs;
    public ShootingStats Stats { get { return _origStats + StatsBuffs; } }
    private ShootingStats _origStats;

    public ShootingState ShootingState = new  ShootingState();

    public void OnUpdate(float dt)
    {
        if (ShootingState.ReloadProgress < 1.0f)
        {
            ShootingState.ReloadProgress += dt / Stats.ReloadSpeed;

            if (ShootingState.ReloadProgress > 1.0f)
                ShootingState.ReloadProgress = 1.0f;
        }
    }
    public void ResetReloadProgress()
    {
        ShootingState.ReloadProgress = 0;
    }
    public AbilityStats(AbilityStatsTemplate t)
    {
        Registerer.RegisterUpdatable(this);
        _origStats = t.Stats;
    }
}
[Serializable]
public class ShootingState
{
    public float ReloadProgress;
}