using UnityEngine;
using UnityEngine.AI;

public class GuardMovement : MonoBehaviour
{
    [SerializeField] Character _character;
    [SerializeField] Transform _walkTarget;
    [SerializeField] float _walkSpeed = 1f;
    [SerializeField] float _runSpeed = 3f;
    float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;
    NavMeshAgent _agent;

    public void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget()
    {
        _agent.speed = _walkSpeed;
        _currentSpeed = _agent.speed;
        _agent.SetDestination(_walkTarget.position);
    }

    public void Persuit()
    {
        _agent.speed = _runSpeed;
        _currentSpeed = _agent.speed;
        _agent.SetDestination(_character.transform.position);
    }

    public bool ReachedTarget()
    {
        return !_agent.pathPending &&
               _agent.remainingDistance <= _agent.stoppingDistance + 0.1f;
    }

    public void Stop()
    {
        _agent.ResetPath();
    }
}