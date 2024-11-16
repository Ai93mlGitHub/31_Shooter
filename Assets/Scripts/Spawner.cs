using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private int _maxInstances = 1;

    private GameObject _spawnObjectPrefab;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private bool _isActive = false;
    private Coroutine _spawnCoroutine;

    public event Action<GameObject> OnObjectSpawned;
    public event Action<GameObject> OnObjectDestroyed;

    public void Initialize(MonoBehaviour spawnObjectPrefab)
    {
        if (spawnObjectPrefab != null)
        {
            _spawnObjectPrefab = spawnObjectPrefab.gameObject;
        }
        else
        {
            Debug.LogError("На объекте нужен MonoBehaviour.");
        }
    }

    public void SpawnObject()
    {
        if (_spawnObjectPrefab == null)
        {
            Debug.LogWarning("Нет префаба спавнера");
            return;
        }

        if (_spawnedObjects.Count < _maxInstances)
        {
            GameObject spawnedObject = Instantiate(_spawnObjectPrefab, transform.position, transform.rotation);
            _spawnedObjects.Add(spawnedObject);

            if (spawnedObject.TryGetComponent<ISpawnable>(out var spawnable))
            {
                spawnable.OnDestroyed += HandleObjectDestroyed;
            }

            OnObjectSpawned?.Invoke(spawnedObject);
        }
    }

    private void HandleObjectDestroyed(GameObject obj)
    {
        _spawnedObjects.Remove(obj);
        OnObjectDestroyed?.Invoke(obj);
    }

    public void ActivateSpawner()
    {
        if (!_isActive)
        {
            _isActive = true;
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
            Debug.Log($"{gameObject.name} спавнер работает.");
        }
    }

    public void DeactivateSpawner()
    {
        if (_isActive)
        {
            _isActive = false;

            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            foreach (var obj in _spawnedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
            _spawnedObjects.Clear();
            Debug.Log($"{gameObject.name} спавнер не работает");
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isActive)
        {
            SpawnObject();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
