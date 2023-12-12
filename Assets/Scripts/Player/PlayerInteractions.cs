using System;
using Counter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerInteractions : MonoBehaviour
    {
        public event Action<EmptyCounter> FocusCounter; 
        
        [SerializeField]
        private float interactionDistance = 1f;
    
        private PlayerInputHandler _playerInputHandler;
        
        private Vector3 _facingDirection;
        
        // @todo consider converting the mechanism to an event based one using Scriptable Objects to decouple
        private EmptyCounter _focusedCounter;
    
        private void Awake()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
        }

        private void Start()
        {
            _playerInputHandler.Interact += PlayerInputOnInteract;
        }

        private void Update()
        {
            DetectInteractions();
        }

        private void DetectInteractions()
        {
            var inputVector = _playerInputHandler.GetNormalizedInputVector();
            var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        
            if (moveDirection != Vector3.zero)
            {
                _facingDirection = moveDirection;
            }
        
            var hitRegistered = Physics.Raycast(transform.position, _facingDirection, out var hit, interactionDistance);
        
            if (!hitRegistered)
            {
                SetFocusedCounter(null);
                return;
            }

            SetFocusedCounter(hit.transform.TryGetComponent(out EmptyCounter emptyCounter) ? emptyCounter : null);
        }
        
        private void SetFocusedCounter(EmptyCounter emptyCounter)
        {
            _focusedCounter = emptyCounter;
            FocusCounter?.Invoke(_focusedCounter);
        }
    
        private void PlayerInputOnInteract(InputAction.CallbackContext obj)
        {
            if (_focusedCounter != null)
            {
                _focusedCounter.Interact();
            }
        }
    }
}
