using UnityEngine;

[CreateAssetMenu(fileName = "DefaultMover", menuName = "Movers/DefaultMover", order = 1)]
public class DefaultMover : Mover
{
    public override void Move(Transform obj, Vector3 dir)
    {
        if (dir.magnitude > MaxSpeed)
        {
            dir = dir.normalized * MaxSpeed;
        }

        obj.Translate(dir * Acceleration * Time.deltaTime);
    }
}