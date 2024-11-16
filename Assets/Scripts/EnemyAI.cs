using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI
{
    [SerializeField] private float _changeDirectionInterval = 5.0f;
    [SerializeField] private float _radiusForMoving = 20f;

    private float _timer;
    private NavMeshAgent _navMeshAgent;

    public EnemyAI(NavMeshAgent navMeshAgent, float changeDirectionInterval)
    {
        _navMeshAgent = navMeshAgent;
        _changeDirectionInterval = changeDirectionInterval;
    }

    public void UpdateAI()
    {
        _timer += Time.deltaTime;

        if (_timer >= _changeDirectionInterval)
        {
            _timer = 0;
            Vector3 newDestination = GetRandomPointOnNavMesh();

            if (newDestination != Vector3.zero)
            {
                _navMeshAgent.SetDestination(newDestination);
            }
        }
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _radiusForMoving;
        randomDirection += _navMeshAgent.transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, _radiusForMoving, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }
}
