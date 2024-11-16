using System;
using UnityEngine;

public class Player : MonoBehaviour, ISpawnable, IDamageable, IShootable
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerShooting _shooting;
    [SerializeField] private Health _health;

    private CharacterController _characterController;
    private InputController _inputController;

    public event Action OnPlayerDeath;
    public event Action<GameObject> OnDestroyed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (_movement != null)
        {
            _movement.Initialize(_characterController, transform);
        }

        if (_health != null)
        {
            _health.OnDeath += HandleDeath;
        }
    }

    private void Start()
    {
        _inputController = FindObjectOfType<InputController>();

        if (_inputController != null)
        {
            _inputController.OnMoveInput += HandleMoveInput;
            _inputController.OnShootInput += HandleShootInput;
        }
    }

    private void HandleShootInput()
    {
        _shooting?.Shoot();
    }

    private void HandleMoveInput(Vector2 moveInput)
    {
        _movement.Rotation(moveInput);
        _movement.Move(moveInput);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void HandleDeath()
    {
        OnPlayerDeath?.Invoke();
        OnDestroyed?.Invoke(gameObject);
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.OnDeath -= HandleDeath;
        }

        if (_inputController != null)
        {
            _inputController.OnMoveInput -= HandleMoveInput;
            _inputController.OnShootInput -= HandleShootInput;
        }
    }
}
