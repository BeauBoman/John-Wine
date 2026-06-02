using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyPathfinding : MonoBehaviour, IUpdatable
{
    public Transform target;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _maxDistance;

    private bool _token;
    private NavMeshAgent _agent;

    void Start()
    {
        _stopDistance = _stopDistance + Random.Range(0, 5f);
        _agent = GetComponent<NavMeshAgent>();
    }

    public void OnUpdate(float deltaTime)
    {
        CheckDistance();
        GetToken();
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
            }
            else
            {
                _agent.SetDestination(transform.position);
            }
        }
        else
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
}
