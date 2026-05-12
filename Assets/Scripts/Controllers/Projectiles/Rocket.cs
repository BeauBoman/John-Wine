using UnityEngine;

public class Rocket : MonoBehaviour, IUpdatable
{
    [SerializeField] private Unit Unit;
    private void Start()
    {
        Registerer.RegisterUpdatable(this);
    }
    public void OnUpdate(float deltaTime)
    {
        Unit.UnitStats.SimComponents.Mover.Move(Unit, -transform.forward, deltaTime);
    }
}
