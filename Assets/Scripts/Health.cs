using System;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    public Action OnDeath { get; internal set; }
    public Action<float> OnDamageTaken { get; set; }

    public Health(float maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        _health -= damage;
        OnDamageTaken?.Invoke(damage);

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }


        Debug.Log($"Урон {damage}");
    }

    public void Heal(float amount)
    {
        if (amount <= 0) return;

        _health += amount;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    private void Die()
    {
        Debug.Log("Смерть");
        OnDeath?.Invoke();
    }

    public bool IsAlive()
    {
        return _health > 0;
    }
}
