using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public class ComponentRuntimeStats
{
    Dictionary<AbilitySO, ModifiableStats<AbilityStats>> AbilityStats = new();
    Dictionary<MoverSO, ModifiableStats<MovementStats>> MoverStats = new();
    Dictionary<EffectSO, ModifiableStats<EffectStats>> EffectStats = new();
    Dictionary<AreaSearchSO, ModifiableStats<AreaSearchStats>> AreaSearchStats = new();
    Dictionary<SensorSO, ModifiableStats<SensorStats>> SensorStats = new();
    Dictionary<RaycasterSO, ModifiableStats<RaycastStats>> RaycasterStats = new();
    Dictionary<TemporaryBehaviorSO, ModifiableStats<TemporaryBehaviorStats>> TemporaryBehaviorStats = new();
    Dictionary<PeriodicBehaviorSO, ModifiableStats<PeriodicBehaviorStats>> PeriodicBehaviorStats = new();

    public void SetComponentsStats(SimulationComponentsPack pack)
    {
        for (int i = pack.Abilities.Count - 1; i >= 0; i--)
        {
            if (pack.Abilities[i] != null) AddStats(pack.Abilities[i]);
        }
        if (pack.Mover != null) AddStats(pack.Mover);
        if (pack.Effect != null) AddStats(pack.Effect);
        if (pack.Sensor != null) AddStats(pack.Sensor);
        if (pack.Raycaster != null) AddStats(pack.Raycaster);

        if (pack.TemporaryBehaviour != null)
        {
            AddStats(pack.TemporaryBehaviour);
        }
        if (pack.PeriodicBehaviour != null)
        {
            AddStats(pack.PeriodicBehaviour);
        }

        if (pack.AreaSearcher != null)
        {
            AddStats(pack.AreaSearcher);
            if (pack.AreaSearcher.Stats.Components.Sensor != null) AddStats(pack.AreaSearcher.Stats.Components.Sensor);
            if (pack.AreaSearcher.Stats.Components.Effect != null) AddStats(pack.AreaSearcher.Stats.Components.Effect);
        }
    }
    public ref readonly AbilityStats GetStats(AbilitySO config) => ref AbilityStats[config].Value;
    public ref readonly MovementStats GetStats(MoverSO config) => ref MoverStats[config].Value;
    public ref readonly EffectStats GetStats(EffectSO config) => ref EffectStats[config].Value;
    public ref readonly AreaSearchStats GetStats(AreaSearchSO config) => ref AreaSearchStats[config].Value;
    public ref readonly SensorStats GetStats(SensorSO config) => ref SensorStats[config].Value;
    public ref readonly RaycastStats GetStats(RaycasterSO config) => ref RaycasterStats[config].Value;
    public ref readonly TemporaryBehaviorStats GetStats(TemporaryBehaviorSO config) => ref TemporaryBehaviorStats[config].Value;
    public ref readonly PeriodicBehaviorStats GetStats(PeriodicBehaviorSO config) => ref PeriodicBehaviorStats[config].Value;
    public ModifiableStats<AbilityStats> GetStatsModifiable(AbilitySO config) => AbilityStats[config];
    public ModifiableStats<MovementStats> GetStatsModifiable(MoverSO config) => MoverStats[config];

    public void AddStats(AbilitySO config)
    {
        if (AbilityStats.ContainsKey(config)) return;

        AbilityStats.Add(config, new ModifiableStats<AbilityStats>(config.Stats));
    }
    public void AddStats(MoverSO config)
    {
        if (MoverStats.ContainsKey(config)) return;

        MoverStats.Add(config, new ModifiableStats<MovementStats>(config.Stats));
    }
    public void AddStats(EffectSO config)
    {
        if (EffectStats.ContainsKey(config)) return;

        EffectStats.Add(config, new ModifiableStats<EffectStats>(config.Stats));
    }
    public void AddStats(AreaSearchSO config)
    {
        if (AreaSearchStats.ContainsKey(config)) return;

        AreaSearchStats.Add(config, new ModifiableStats<AreaSearchStats>(config.Stats));
    }
    public void AddStats(SensorSO config)
    {
        if (SensorStats.ContainsKey(config)) return;

        SensorStats.Add(config, new ModifiableStats<SensorStats>(config.Stats));
    }
    public void AddStats(RaycasterSO config)
    {
        if (RaycasterStats.ContainsKey(config)) return;

        RaycasterStats.Add(config, new ModifiableStats<RaycastStats>(config.Stats));
    }
    public void AddStats(TemporaryBehaviorSO config)
    {
        if (TemporaryBehaviorStats.ContainsKey(config)) return;

        TemporaryBehaviorStats.Add(config, new ModifiableStats<TemporaryBehaviorStats>(config.Stats));
    }
    public void AddStats(PeriodicBehaviorSO config)
    {
        if (PeriodicBehaviorStats.ContainsKey(config)) return;

        PeriodicBehaviorStats.Add(config, new ModifiableStats<PeriodicBehaviorStats>(config.Stats));
    }
}

public class ModifiableStats<T> where T : struct
{
    private readonly T _baseValue;
    private T _buffValue;
    public ref readonly T Value => ref _value;

    private T _value;

    private static readonly Func<T, T, T> AddOperation;
    private static readonly Func<T, float, T> MultiplyOperation;

    static ModifiableStats()
    {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

        var paramAddA = Expression.Parameter(typeof(T), "a");
        var paramAddB = Expression.Parameter(typeof(T), "b");
        MemberBinding[] addBindings = new MemberBinding[fields.Length];

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
            var fieldA = Expression.Field(paramAddA, field);
            var fieldB = Expression.Field(paramAddB, field);

            if (field.FieldType == typeof(float) || field.FieldType == typeof(double) || field.FieldType == typeof(int))
            {
                var addField = Expression.Add(fieldA, fieldB);
                addBindings[i] = Expression.Bind(field, addField);
            }
            else
            {
                addBindings[i] = Expression.Bind(field, fieldA);
            }
        }

        var addMemberInit = Expression.MemberInit(Expression.New(typeof(T)), addBindings);
        AddOperation = Expression.Lambda<Func<T, T, T>>(addMemberInit, paramAddA, paramAddB).Compile();


        var paramStruct = Expression.Parameter(typeof(T), "s");
        var paramFloat = Expression.Parameter(typeof(float), "f");
        MemberBinding[] multiplyBindings = new MemberBinding[fields.Length];

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
            var fieldAccess = Expression.Field(paramStruct, field);

            if (field.FieldType == typeof(float) || field.FieldType == typeof(double))
            {
                var multiplyField = Expression.Multiply(fieldAccess, paramFloat);
                multiplyBindings[i] = Expression.Bind(field, multiplyField);
            }
            else if (field.FieldType == typeof(int))
            {
                var multiplyField = Expression.Multiply(Expression.Convert(fieldAccess, typeof(float)), paramFloat);
                var castToInt = Expression.Convert(multiplyField, typeof(int));
                multiplyBindings[i] = Expression.Bind(field, castToInt);
            }
            else
            {
                multiplyBindings[i] = Expression.Bind(field, fieldAccess);
            }
        }

        var multiplyMemberInit = Expression.MemberInit(Expression.New(typeof(T)), multiplyBindings);
        MultiplyOperation = Expression.Lambda<Func<T, float, T>>(multiplyMemberInit, paramStruct, paramFloat).Compile();
    }

    public ModifiableStats(T baseValue)
    {
        _baseValue = baseValue;
        _buffValue = default;
        UpdateValue();
    }

    public void BuffAdd(T buff)
    {
        _buffValue = AddOperation(_buffValue, buff);
        UpdateValue();
    }

    public void BuffMultiply(float multiplier)
    {
        T buffDelta = MultiplyOperation(_baseValue, multiplier - 1f);
        _buffValue = AddOperation(_buffValue, buffDelta);
        UpdateValue();
    }

    private void UpdateValue() => _value = AddOperation(_baseValue, _buffValue);
}