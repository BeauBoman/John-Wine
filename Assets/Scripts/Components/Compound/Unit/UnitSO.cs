using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit SO", menuName = "Components/Compound/Unit/Unit SO")]
public class UnitSO : ScriptableObject
{
    [Header("General")]
    [SerializeReference] public StatsTemplate StatsTemplate;
    [SerializeField] public SimulationComponentsPack SimComponents;
    [SerializeReference] public Tags Tags;
}
[Serializable]
public struct SimulationComponentsPack
{
    [SerializeReference] public MoverSO Mover;
    [SerializeReference] public SensorSO Sensor;
    [SerializeReference] public RaycasterSO Raycaster;
    [SerializeReference] public AreaSearchSO AreaSearcher;
    [SerializeReference] public List<AbilitySO> Abilities;
    [SerializeReference] public Spawner<Unit> UnitSpawner;
    [SerializeReference] public EffectSO Effect;
    [SerializeReference] public BehaviorSO Behaviour;
}