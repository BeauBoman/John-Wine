using UnityEngine;

[CreateAssetMenu(fileName = "CompositeMover", menuName = "Components/Movers/CompositeMover", order = 1)]
public class CompositeMover : Mover
{
    public Mover[] Movers;
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        for(int i = 0; i < Movers.Length; i++)
            Movers[i].Move(unit, dir, dt);
    }
}
