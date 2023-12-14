using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
        public event Action<InputAction.CallbackContext> Interact;
        public event Action<InputAction.CallbackContext> InteractAlt;
    
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        
            _playerInputActions.Player.Interact.performed += ctx => Interact?.Invoke(ctx);
            _playerInputActions.Player.InteractAlt.performed += ctx => InteractAlt?.Invoke(ctx);
        }

        public Vector2 GetInputVector()
        {
            return _playerInputActions.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetNormalizedInputVector()
        {
            var inputVector = GetInputVector();
        
            inputVector.Normalize();
        
            return inputVector;
        }
    }
}
