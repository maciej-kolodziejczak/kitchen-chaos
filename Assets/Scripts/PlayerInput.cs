using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action<InputAction.CallbackContext> OnInteract;
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += ctx => OnInteract?.Invoke(ctx);
    }

    public Vector2 GetNormalizedInputVector()
    {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        
        inputVector.Normalize();

        return inputVector;
    }
}
