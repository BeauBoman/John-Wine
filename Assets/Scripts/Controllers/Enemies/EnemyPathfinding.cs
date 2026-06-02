using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyPathfinding : MonoBehaviour, IUpdatable
{
    [HideInInspector] public Unit unit;
    public Transform playerTarget;
    public Transform flanks;
    [SerializeField] private float _maxDistance;

    private List<Transform> _flankTarget = new();
    private bool _token;
    private int _targetIndex;
    private float _ignoreFlank;
    private NavMeshAgent _agent;
    void Start()
    {
        foreach (Transform child in flanks)
        {
            _flankTarget.Add(child);
        }

        unit = GetComponent<Unit>();
        Registerer.RegisterUpdatable(this);
        _agent = GetComponent<NavMeshAgent>();
        _targetIndex = Random.Range(0, _flankTarget.Count);
        _ignoreFlank = Vector3.Distance(playerTarget.position, _flankTarget[_targetIndex].position);

        TokenManager.instance._enemies.Add(gameObject, this);
    }

    public void OnUpdate(float deltaTime)
    {
        GetToken();
        CheckDistance();
    }
    private void GetToken()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= _ignoreFlank)
        {
            if (!_token)
            {
                _token = TokenManager.instance.RequestToken(gameObject, distanceToPlayer);
            }
            if (_token)
            {
                if (distanceToPlayer <= _ignoreFlank)
                {
                    _agent.SetDestination(playerTarget.position);
                } else
                    _agent.SetDestination(_flankTarget[_targetIndex].position);
            }
            else
            {
                _agent.SetDestination(_flankTarget[_targetIndex].position);
            }
        }
        else
        {
            _agent.SetDestination(_flankTarget[_targetIndex].position);

            if (_token && distanceToPlayer > _ignoreFlank + 1f)
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
        if (distance > _maxDistance)
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
