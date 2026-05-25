using System;
using UnityEngine;

public abstract class BehaviorSO : ScriptableObject
{
    protected virtual TemporaryBehavior CreateTempBehavior(Unit targetUnit) { return null; }
    protected virtual PeriodicBehavior CreatePeriodBehavior(Unit targetUnit) { return null; }
    public abstract void ApplyBehavior(Unit targetUnit);
}
public abstract class TemporaryBehaviorSO : BehaviorSO
{
    [SerializeField] public TemporaryBehaviorStats Stats;
    [SerializeField] public SimulationComponentsPack StartComponents;
    [SerializeField] public SimulationComponentsPack EndComponents;
}
public abstract class PeriodicBehaviorSO : BehaviorSO
{
    [SerializeField] public PeriodicBehaviorStats Stats;
    [SerializeField] public SimulationComponentsPack PeriodicComponents;
}
[Serializable]
public struct TemporaryBehaviorStats
{
    public float Duration;
    public static TemporaryBehaviorStats operator +(TemporaryBehaviorStats a, TemporaryBehaviorStats b)
    {
        return new TemporaryBehaviorStats()
        {
            Duration = a.Duration + b.Duration
        };
    }
}
public struct PeriodicBehaviorStats
{
    public float Duration;
    public float Period;
    public static PeriodicBehaviorStats operator +(PeriodicBehaviorStats a, PeriodicBehaviorStats b)
    {
        return new PeriodicBehaviorStats()
        {
            Duration = a.Duration + b.Duration,
            Period = a.Period + b.Period
        };
    }
}
