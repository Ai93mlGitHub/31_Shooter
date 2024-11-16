using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;
    public event Action OnShootInput;

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput != Vector2.zero)
        {
            OnMoveInput?.Invoke(moveInput);
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnShootInput?.Invoke();
        }
    }
}
