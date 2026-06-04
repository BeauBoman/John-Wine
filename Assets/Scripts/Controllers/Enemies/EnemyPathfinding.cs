using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyPathfinding : MonoBehaviour, IUpdatable
{
    [HideInInspector] public Unit unit;
    public Transform playerTarget;
    [HideInInspector] public bool _token;

    [SerializeField] private PathfindingStats _stats;

    private NavMeshAgent _agent;
    private Vector3 _flank;
    private Vector3 _modifier;
    private float _threshold;
    private Vector3 _groundedPlayerPos;
    void Start()
    {
        unit = GetComponent<Unit>();
        Registerer.RegisterUpdatable(this);
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;

        TokenManager.instance._enemies.Add(gameObject, this);

        Vector2 randomCircle = Random.insideUnitCircle.normalized;
        _modifier = new Vector3(randomCircle.x, 0, randomCircle.y) * Random.Range(_stats.minDistanceFlank, _stats.maxDistanceFlank);
        _threshold = _modifier.magnitude;
    }

    public void OnUpdate(float deltaTime)
    {
        if (playerTarget == null) return;
        if (_agent == null) return;
        if (!_agent.isActiveAndEnabled || !_agent.isOnNavMesh) return;

        _agent.nextPosition = transform.position;

        if (NavMesh.SamplePosition(playerTarget.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            _groundedPlayerPos = hit.position;
        }
        else
        {
            _groundedPlayerPos = playerTarget.position;
        }

        _flank = new Vector3(playerTarget.position.x + _modifier.x, _groundedPlayerPos.y, playerTarget.position.z + _modifier.z);

        GetToken();
        Mover(deltaTime);
    }
    private void GetToken()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= _threshold + 0.5f)
        {
            if (!_token)
            {
                _token = TokenManager.instance.RequestToken(gameObject, distanceToPlayer + _stats.tokenPriority);
            }
            if (_token)
            {
                _agent.SetDestination(playerTarget.position);
            }
            else
            {
                _agent.SetDestination(_flank);
            }
        }
        else
        {
            _agent.SetDestination(_flank);

            if (_token && distanceToPlayer > _threshold + 1f)
            {
                ReleaseToken();
            }
        }
    }
    private void Mover(float dt)
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);
        unit.UnitSO.SimComponents.Movers.RotationalMover.Move(unit, playerTarget.position, dt);

        if (distance > _stats.stoppingDistance && distance < _stats.maxDistanceSearch)
        {
            if (_agent.pathPending == false && _agent.remainingDistance < 0.2f)
            {
                unit.UnitSO.SimComponents.Movers.Mover.Move(unit, Vector3.zero, dt);
            }
            else
            {
                unit.UnitSO.SimComponents.Movers.Mover.Move(unit, _agent.desiredVelocity.normalized, dt);
            }
        } else
        {
            unit.UnitSO.SimComponents.Movers.Mover.Move(unit, Vector3.zero, dt);
        }
    }
    public void ReleaseToken()
    {
        if (_token)
        {
            if (TokenManager.instance._activeTokens.ContainsKey(gameObject))
            {
                TokenManager.instance._activeTokens.Remove(gameObject);
                _token = false;
            }
        }
    }
    public void Death()
    {
        ReleaseToken();
        Registerer.UnregisterUpdatable(this);
    }
}
