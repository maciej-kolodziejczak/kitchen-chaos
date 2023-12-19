using System;
using Counter;
using Product;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(ProductHandler))]
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayers;
        [SerializeField] private float interactionDistance = 2f;
        
        private PlayerInputHandler _inputHandler;
        private ProductHandler _productHandler;
        
        private BaseCounter _interactable;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();
            _productHandler = GetComponent<ProductHandler>();

            _inputHandler.Moving += MovingEventHandler;
            _inputHandler.Interacted += InteractedEventHandler;
            _inputHandler.Used += UsedEventHandler;
        }

        private void Update()
        {
            var canInteract = Physics.Raycast(transform.position, _moveDirection, out var hit, interactionDistance,
                interactableLayers);

            if (!canInteract)
            {
                if (!_interactable) return;
                
                _interactable.Blur();
                _interactable = null;
                
                return;
            }

            var hasInteractable = hit.collider.TryGetComponent<BaseCounter>(out var interactable);
            
            if (!hasInteractable) return;

            // @todo clean up conditions
            if (_interactable)
            {
                if (_interactable == interactable) return;
                
                _interactable.Blur();
            }
            
            _interactable = interactable;
            _interactable.Focus();
        }

        private void MovingEventHandler(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero) return;
            
            _moveDirection = new Vector3(movementInput.x, 0, movementInput.y);   
        }
        
        private void InteractedEventHandler()
        {
            if (_interactable == null) return;
            
            _interactable.Interact(_productHandler);
        }
        
        private void UsedEventHandler()
        {
            if (_interactable == null) return;
            
            _interactable.Use();
        }
    }
}