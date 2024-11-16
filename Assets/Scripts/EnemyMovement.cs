using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement
{
    private NavMeshAgent _navMeshAgent;

    public EnemyMovement(NavMeshAgent navMeshAgent)
    {
        _navMeshAgent = navMeshAgent;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (_navMeshAgent != null && _navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.SetDestination(targetPosition);
        }
    }
}
