using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ISpawnable, IDamageable
{
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private Health _health;
    [SerializeField] private DamageDealer _damageDealer;

    private EnemyMovement _enemyMovement;
    private NavMeshAgent _navMeshAgent;

    public event Action<GameObject> OnDestroyed;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyMovement = new EnemyMovement(_navMeshAgent);
        _enemyAI = new EnemyAI(_navMeshAgent, 5.0f);

        _health.OnDeath += HandleDeath;
    }

    private void Update()
    {
        _enemyAI.UpdateAI();
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void HandleDeath()
    {
        OnDestroyed?.Invoke(gameObject);  // Оповещаем о разрушении объекта
        Destroy(gameObject);
        Debug.Log("Enemy destroyed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player) && other.TryGetComponent<IDamageable>(out var damageable))
        {
            _damageDealer.DealDamage(damageable);
        }
    }

    private void OnDestroy()
    {
        _health.OnDeath -= HandleDeath;
    }
}
