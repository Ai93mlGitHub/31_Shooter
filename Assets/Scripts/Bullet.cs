using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private float _autoDestroyDelay = 1f;

    void Start()
    {
        StartCoroutine(AutoDestroyAfterDelay());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private IEnumerator AutoDestroyAfterDelay()
    {
        yield return new WaitForSeconds(_autoDestroyDelay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() is Enemy enemy)
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
            return;
        }

        if (other.GetComponent<Player>() is not Player)
        {
            Destroy(gameObject);
        }
    }
}
