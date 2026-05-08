using UnityEngine;

public class Rocket : Unit, IUpdatable
{
    public void OnUpdate(float deltaTime)
    {
        Components.Mover.Move(this, transform.forward, deltaTime);
    }
}
