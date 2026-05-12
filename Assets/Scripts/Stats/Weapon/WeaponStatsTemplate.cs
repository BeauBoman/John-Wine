using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stats Template", menuName = "Stats/Weapon Stats Template", order = 1)]
public class WeaponStatsTemplate : ScriptableObject
{
    [SerializeField] public ShootingStats Stats;
}
[Serializable]
public struct ShootingStats
{
    public float ReloadSpeed;
    public float Range;

    public static ShootingStats operator +(ShootingStats a, ShootingStats b)
    {
        return new ShootingStats()
        {
            ReloadSpeed = a.ReloadSpeed + b.ReloadSpeed,
            Range = a.Range + b.Range
        };
    }
}