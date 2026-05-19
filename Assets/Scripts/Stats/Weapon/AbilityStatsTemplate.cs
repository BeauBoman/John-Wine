using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability Stats Template", menuName = "Stats/Ability Stats Template", order = 1)]
public class AbilityStatsTemplate : ScriptableObject
{
    [SerializeField] public ShootingStats Stats;
}
[Serializable]
public struct ShootingStats
{
    public float ReloadSpeed;
    public float Range;
    public Vector3 ExplosionSize;
    public static ShootingStats operator +(ShootingStats a, ShootingStats b)
    {
        return new ShootingStats()
        {
            ReloadSpeed = a.ReloadSpeed + b.ReloadSpeed,
            Range = a.Range + b.Range,
            ExplosionSize = a.ExplosionSize + b.ExplosionSize
        };
    }
}