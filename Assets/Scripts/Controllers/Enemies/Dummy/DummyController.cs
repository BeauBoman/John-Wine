using UnityEngine;

public sealed class DummyController : Controller, IUpdatable
{
    private void Start() => _unit.OnSpawn();
    public override void OnStart()
    {
        _unit.OnHealthIsZero += Death;
        Registerer.RegisterUpdatable(this);
    }
    public void OnUpdate(float dt)
    {
        _unit.OnUpdate(dt);
    }
    public void Death()
    {
        _unit.OnHealthIsZero -= Death;
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
