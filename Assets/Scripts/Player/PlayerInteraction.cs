using System;
using Counter;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayers;
        [SerializeField] private float interactionDistance = 2f;
        
        private PlayerInputHandler _inputHandler;
        private BaseCounter _interactable;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();

            _inputHandler.Moving += MovingEventHandler;
            _inputHandler.Interacted += InteractedEventHandler;
        }

        private void Update()
        {
            var canInteract = Physics.Raycast(transform.position, _moveDirection, out var hit, interactionDistance, interactableLayers);
            
            if (!canInteract) return;
            
            var hasInteractable = hit.collider.TryGetComponent<BaseCounter>(out var interactable);
            
            if (!hasInteractable) return;
            
            _interactable = interactable;
        }

        private void MovingEventHandler(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero) return;
            
            _moveDirection = new Vector3(movementInput.x, 0, movementInput.y);   
        }
        
        private void InteractedEventHandler()
        {
            if (_interactable == null) return;
            
            _interactable.Interact();
        }
    }
}