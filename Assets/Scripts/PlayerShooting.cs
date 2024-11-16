using UnityEngine;

[System.Serializable]
public class PlayerShooting
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootingPoint;

    public void Shoot()
    {
        if (_bulletPrefab != null && _shootingPoint != null)
        {
            Bullet bullet = Object.Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Не указан префаб пули");
        }
    }
}
