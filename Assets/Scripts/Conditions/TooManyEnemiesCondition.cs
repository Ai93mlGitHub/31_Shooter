using UnityEngine;

[System.Serializable]

public class TooManyEnemiesCondition : ILoseCondition
{
    public event System.Action OnLose;

    private int _maxEnemyCount = 5;
    
    private int _currentEnemyCount = 0;


    public void RegisterEnemySpawned()
    {
        _currentEnemyCount++;
        Debug.Log($"Enemies: {_currentEnemyCount}");

        if (_currentEnemyCount > _maxEnemyCount)
        {
            OnLose?.Invoke();
        }
    }

    public void RegisterEnemyDestroyed()
    {
        _currentEnemyCount--;
        Debug.Log($"Enemies: {_currentEnemyCount}");
    }

    public void UpdateCondition() { }
}