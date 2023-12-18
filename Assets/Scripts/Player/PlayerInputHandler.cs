using System;
using UnityEngine;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public event Action<Vector2> Moving;
        public event Action Interacted;
        public event Action InteractedAlt;
    
        private PlayerInputActions _playerControls;

        private void Awake()
        {
            _playerControls = new PlayerInputActions();
            _playerControls.Enable();
        
            _playerControls.Player.Interact.performed += ctx => Interacted?.Invoke();
            _playerControls.Player.InteractAlt.performed += ctx => InteractedAlt?.Invoke();
        }
    
        private void Update()
        {
            var movementInput = _playerControls.Player.Move.ReadValue<Vector2>();
            Moving?.Invoke(movementInput);
        }
    }
}
