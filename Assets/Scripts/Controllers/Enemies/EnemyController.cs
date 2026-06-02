using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyController : Controller, IUpdatable
{
    [SerializeField] private Unit _unit;
    public Transform target;
    [SerializeField] private float _stopDistance;

    private NavMeshAgent _agent;
    private bool _token;
    private void Start() => _unit.OnSpawn();
    public override void OnStart()
    {
        _unit.OnHealthIsZero += Death;
        Registerer.RegisterUpdatable(this);

        _agent = GetComponent<NavMeshAgent>();

        _stopDistance = _stopDistance + Random.Range(0, 5f);
    }
    public void OnUpdate(float dt)
    {
        _unit.OnUpdate(dt);
        GetToken();
        CheckDistance();
    }
    public void Death()
    {
        _unit.OnHealthIsZero -= Death;
        Registerer.UnregisterUpdatable(this);

        ReleaseToken();
        Destroy(gameObject);
    }
    private void GetToken()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= _stopDistance)
        {
            if (!_token)
            {
                _token = TokenManager.instance.RequestToken(gameObject, distanceToPlayer);
            }
            if (_token)
            {
                _agent.SetDestination(target.position);
            } else
            {
                _agent.SetDestination(transform.position);
            }
        } else
        {
            _agent.SetDestination(target.position);
            
            if (_token && distanceToPlayer > _stopDistance + 1f)
            {
                ReleaseToken();
            }
        }
    }
    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 2f)
        {
            _agent.SetDestination(transform.position);
        }
    }
    public void ReleaseToken()
    {
        if (_token)
        {
            TokenManager.instance.RemoveToken(gameObject);
            _token = false;
        }
    }
}
