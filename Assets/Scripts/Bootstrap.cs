using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Spawner _playerSpawner;
    [SerializeField] private Spawner _enemySpawner;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private WinConditionType _winConditionType;
    [SerializeField] private LoseConditionType _loseConditionType;

    private IWinCondition _winCondition;
    private ILoseCondition _loseCondition;

    private bool _isGameStopped = false;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        _playerSpawner.Initialize(_playerPrefab);
        _enemySpawner.Initialize(_enemyPrefab);

        _playerSpawner.OnObjectSpawned += OnPlayerSpawned;
        _enemySpawner.OnObjectSpawned += OnEnemySpawned;
        _enemySpawner.OnObjectDestroyed += OnEnemyDestroyed;

        InitializeConditions();

        if (_winCondition != null) 
            _winCondition.OnWin += HandleWin;

        if (_loseCondition != null) 
            _loseCondition.OnLose += HandleLose;

        ActivateSpawners();
    }

    private void InitializeConditions()
    {
        switch (_winConditionType)
        {
            case WinConditionType.SurviveForTime:
                _winCondition = new SurviveForTimeCondition();
                break;

            case WinConditionType.KillEnemies:
                _winCondition = new KillEnemiesCondition();
                break;
        }

        switch (_loseConditionType)
        {
            case LoseConditionType.PlayerDied:
                _loseCondition = new PlayerDiedCondition();
                break;

            case LoseConditionType.TooManyEnemies:
                _loseCondition = new TooManyEnemiesCondition();
                break;
        }
    }

    private void Update()
    {
        if (_isGameStopped) 
            return;

        _winCondition?.UpdateCondition();
        _loseCondition?.UpdateCondition();
    }

    private void OnPlayerSpawned(GameObject playerObject)
    {
        var player = playerObject.GetComponent<Player>();
        player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnEnemySpawned(GameObject enemyObject)
    {
        Debug.Log("Враг заспавнен");

        if (_winCondition is KillEnemiesCondition killEnemiesCondition)
        {
            enemyObject.GetComponent<Enemy>().OnDestroyed += (enemy) => killEnemiesCondition.RegisterEnemyKilled();
        }

        if (_loseCondition is TooManyEnemiesCondition tooManyEnemiesCondition)
        {
            tooManyEnemiesCondition.RegisterEnemySpawned();
            enemyObject.GetComponent<Enemy>().OnDestroyed += (enemy) => tooManyEnemiesCondition.RegisterEnemyDestroyed();
        }
    }

    private void OnEnemyDestroyed(GameObject enemyObject)
    {
        Debug.Log("Враг убит");
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Конец игры игрок метрв");
        if (_loseCondition is PlayerDiedCondition playerDiedCondition)
        {
            playerDiedCondition.RegisterPlayerDied();
        }
    }

    private void HandleWin()
    {
        Debug.Log("Победа");
        StopGame();
    }

    private void HandleLose()
    {
        Debug.Log("Проигрыш");
        StopGame();
    }

    public void StopGame()
    {
        if (_isGameStopped) 
            return;

        _isGameStopped = true;

        DeactivateSpawners();

        if (_winCondition != null) 
            _winCondition.OnWin -= HandleWin;

        if (_loseCondition != null) 
            _loseCondition.OnLose -= HandleLose;

        Debug.Log("Стоп игра");
    }

    public void Play()
    {
        if (!_isGameStopped) 
            return;

        _isGameStopped = false;

        InitializeConditions();

        if (_winCondition != null) _winCondition.OnWin += HandleWin;
        if (_loseCondition != null) _loseCondition.OnLose += HandleLose;

        ActivateSpawners();

        Debug.Log("Старт игры");
    }

    public void ToggleGamePause()
    {
        _isGameStopped = !_isGameStopped;
        Debug.Log(_isGameStopped ? "Пауза" : "Возобновление");
    }

    private void ActivateSpawners()
    {
        _playerSpawner.ActivateSpawner();
        _enemySpawner.ActivateSpawner();
    }

    private void DeactivateSpawners()
    {
        _playerSpawner.DeactivateSpawner();
        _enemySpawner.DeactivateSpawner();
    }
}
