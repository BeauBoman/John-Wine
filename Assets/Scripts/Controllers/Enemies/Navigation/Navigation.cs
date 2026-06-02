using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform target;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (target != null)
        {
            _agent.SetDestination(target.position);
        }
    }
}
