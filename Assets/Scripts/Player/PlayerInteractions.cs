using System;
using Counter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerKitchenObjectInteraction))]
    public class PlayerInteractions : MonoBehaviour
    {
        public event Action<BaseCounter> FocusCounter; 
        
        public static PlayerInteractions Instance { get; private set; }
        
        [SerializeField]
        private float interactionDistance = 1f;
        [SerializeField]
        private LayerMask interactionLayerMask;
    
        private PlayerInputHandler _playerInputHandler;
        private Vector3 _facingDirection;
        private BaseCounter _focusedCounter;
    
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
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
        
            var hitRegistered = Physics.Raycast(transform.position, _facingDirection, out var hit, interactionDistance, interactionLayerMask);
        
            if (!hitRegistered)
            {
                SetFocusedCounter(null);
                return;
            }

            if (!hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetFocusedCounter(null);
                return;
            }
            
            SetFocusedCounter(baseCounter);
        }
        
        private void SetFocusedCounter(BaseCounter baseCounter)
        {
            _focusedCounter = baseCounter;
            FocusCounter?.Invoke(_focusedCounter);
        }
    
        private void PlayerInputOnInteract(InputAction.CallbackContext obj)
        {
            if (_focusedCounter != null)
            {
                _focusedCounter.Interact(this);
            }
        }
    }
}
