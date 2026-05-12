using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Unit Spawner", menuName ="Components/Simulation/Spawner/Unit Spawner")]
public class UnitSpawner : Spawner<Unit>
{
    public event Action<Unit> OnSpawn;
    public override Unit Spawn(PositionArgs args)
    {
        Unit spawned = Instantiate(_prefab, args.position + PositionOffset, args.rotation * RotationOffset);
        OnSpawn?.Invoke(spawned);
        return spawned;
    }
}
