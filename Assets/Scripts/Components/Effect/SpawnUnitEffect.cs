using UnityEngine;

public class SpawnUnitEffect : Effect
{
    public override Unit Execute(Unit unit)
    {
        Unit spawned = Instantiate(unit);
        return spawned;
    }
}
