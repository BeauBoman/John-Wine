using UnityEngine;

[CreateAssetMenu(fileName = "CCDefaultMover", menuName = "Components/Movers/CharacterController/CCDefaultMover", order = 1)]
public class CCDefaultMover : Mover
{
    public override void Move(Unit unit, Vector3 dir, float dt)
    {
        MovementStats m = unit.Stats.Movement;
        unit.Refs.CC.Move(dt * dir);
    }
}