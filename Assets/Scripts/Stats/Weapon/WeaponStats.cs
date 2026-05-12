using System;

public class WeaponStats : IUpdatable
{
    public ShootingStats StatsBuffs;
    public ShootingStats Stats { get { return origStats + StatsBuffs; } }

    private ShootingStats origStats;
    public ShootingState ShootingState;

    public void OnUpdate(float dt)
    {
        if (ShootingState.ReloadProgress < 1.0f)
        {
            ShootingState.ReloadProgress += dt / Stats.ReloadSpeed;

            if (ShootingState.ReloadProgress > 1.0f)
                ShootingState.ReloadProgress = 1.0f;
        }
    }
    public WeaponStats(WeaponStatsTemplate t)
    {
        Registerer.RegisterUpdatable(this);
        origStats = t.Stats;
    }
}
[Serializable]
public struct ShootingState
{
    public float ReloadProgress;
}