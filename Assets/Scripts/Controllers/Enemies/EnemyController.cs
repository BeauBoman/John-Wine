using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyController : Controller, IUpdatable
{
    [SerializeField] private Unit _unit;
    private NavMeshAgent _agent;
    public Transform target;
    private void Start() => _unit.OnSpawn();
    public override void OnStart()
    {
        _unit.OnHealthIsZero += Death;
        Registerer.RegisterUpdatable(this);

        _agent = GetComponent<NavMeshAgent>();
    }
    public void OnUpdate(float dt)
    {
        _unit.OnUpdate(dt);

        if (target != null)
        {
            _agent.SetDestination(target.position);
        }
    }
    public void Death()
    {
        _unit.OnHealthIsZero -= Death;
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
