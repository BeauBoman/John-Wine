using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Components/Containers/Unit/Default Unit")]
public class DefaultUnit : ScriptableObject
{
    [Header("General")]
    [SerializeField] public SimulationComponentsPack SimComponents;
    [SerializeReference] public Weapon Weapon;
    [SerializeReference] public Tags Tags;
}
[Serializable]
public struct SimulationComponentsPack
{
    [SerializeReference] public Mover Mover;
    [SerializeReference] public Effect Effect;
    [SerializeReference] public Behaviour Behaviour;
    [SerializeReference] public Spawner<Unit> UnitSpawner;
    [SerializeReference] public AreaSearch AreaSearcher;
    [SerializeReference] public Raycaster Raycaster;
}