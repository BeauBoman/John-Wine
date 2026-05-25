using UnityEngine;

[CreateAssetMenu(fileName = "CompositeMover", menuName = "Components/Simulation/Mover/CompositeMover", order = 1)]
public class CompositeMover : MoverSO
{
    public MoverSO[] Movers;
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        for(int i = 0; i < Movers.Length; i++)
            Movers[i].Move(unit, dir, dt);
    }
}
