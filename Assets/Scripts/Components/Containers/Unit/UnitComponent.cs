using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Components/Containers/Unit/Unit Component")]
public class UnitComponent : ScriptableObject
{
    [Header("General")]
    [SerializeReference] public StatsTemplate StatsTemplate;
    [SerializeField] public SimulationComponentsPack SimComponents;
    [SerializeReference] public Ability Ability;
    [SerializeReference] public Tags Tags;
}
[Serializable]
public struct SimulationComponentsPack
{
    [SerializeReference] public Mover Mover;
    [SerializeReference] public Effect Effect;
    [SerializeReference] public Behaviour Behaviour;

    [SerializeReference] public Spawner<Unit> UnitSpawner;

    [SerializeReference] public Sensor Sensor;
    [SerializeReference] public AreaSearch AreaSearcher;
    [SerializeReference] public Raycaster Raycaster;
}