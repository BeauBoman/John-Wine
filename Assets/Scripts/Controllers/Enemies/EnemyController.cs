using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyController : Controller, IUpdatable
{
    [SerializeField] private Unit _unit;
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

        gameObject.GetComponent<EnemyPathfinding>().Death();
        Destroy(gameObject);
    }
}
