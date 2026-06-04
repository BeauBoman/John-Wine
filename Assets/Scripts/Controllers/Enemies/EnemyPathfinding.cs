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

    [SerializeField] private float _maxDistanceSearch;
    [SerializeField] private float _minDistanceFlank;
    [SerializeField] private float _maxDistanceFlank;
    [Tooltip("Default: 0")]
    [SerializeField] private float _tokenAdditionalPriority;

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
        TokenManager.instance._enemies.Add(gameObject, this);

        Vector2 randomCircle = Random.insideUnitCircle.normalized;
        _modifier = new Vector3(randomCircle.x, 0, randomCircle.y) * Random.Range(_minDistanceFlank, _maxDistanceFlank);
        _threshold = _modifier.magnitude;

        if (_tokenAdditionalPriority != 0)
            _tokenAdditionalPriority = _tokenAdditionalPriority * -1f;
    }

    public void OnUpdate(float deltaTime)
    {
        if (playerTarget == null) return;
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
        CheckDistance();
    }
    private void GetToken()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= _threshold + 0.5f)
        {
            if (!_token)
            {
                _token = TokenManager.instance.RequestToken(gameObject, distanceToPlayer + _tokenAdditionalPriority);
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
    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance < 2f)
        {
            _agent.SetDestination(transform.position);
        }
        if (distance > _maxDistanceSearch)
        {
            _agent.SetDestination(transform.position);
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
