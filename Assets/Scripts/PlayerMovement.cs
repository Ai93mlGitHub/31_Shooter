using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _characterController;
    private Transform _characterTransform;
    private float _verticalVelocity = 0f;
    private Vector3 _moveDirection;

    public void Initialize(CharacterController controller, Transform transform)
    {
        _characterController = controller;
        _characterTransform = transform;
    }

    public void Rotation(Vector2 input)
    {
        _moveDirection = new Vector3(input.x, 0, input.y);
        _characterTransform.rotation = Quaternion.LookRotation(_moveDirection);
    }

    public void Move(Vector2 input)
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = 0f;
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        Vector3 forwardMovement = _characterTransform.forward * input.magnitude * _speed;
        forwardMovement.y = _verticalVelocity;
        _characterController.Move(forwardMovement * Time.deltaTime);
    }
}
