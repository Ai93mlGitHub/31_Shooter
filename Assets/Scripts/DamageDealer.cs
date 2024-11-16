using UnityEngine;

[System.Serializable]
public class DamageDealer
{
    [SerializeField] private float _damageAmount;

    public void DealDamage(IDamageable target)
    {
        target.TakeDamage(_damageAmount);
    }
}
