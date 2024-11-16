using UnityEngine;

public class KillEnemiesCondition : IWinCondition
{
    public event System.Action OnWin;

    private int _enemiesToKill = 10;
    
    private int _enemiesKilled;

    public void RegisterEnemyKilled()
    {
        _enemiesKilled++;

        if (_enemiesKilled >= _enemiesToKill)
        {
            OnWin?.Invoke();
        }

        Debug.Log($"Enemies killed: {_enemiesKilled}");
    }

    public void UpdateCondition() { }
}