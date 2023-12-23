using Common;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Holder))]
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 1f;
        [SerializeField] private LayerMask interactableLayers;
    
        private Holder _holder;
        private PlayerInputHandler _inputHandler;
        private Vector3 _castDirection;
        private IInteractable<IHolder> _interactable;
    
        private void Awake()
        {
            _holder = GetComponent<Holder>();
            _inputHandler = GetComponent<PlayerInputHandler>(); ;
        }

        private void Start()
        {
            _inputHandler.Moving += MovingEventHandler;
            _inputHandler.Interacted += InteractedEventHandler;
            _inputHandler.Used += UseEventHandler;
        }

        private void Update()
        {
            var isHit = Physics.Raycast(transform.position, _castDirection, out var hit, interactDistance, interactableLayers);
        
            if (!isHit)
            {
                _interactable?.Blur();
                _interactable = null;
                return;
            };
        
            var canInteract = hit.collider.TryGetComponent<IInteractable<IHolder>>(out var interactable);
        
            if (!canInteract) return;

            _interactable = interactable;
            _interactable.Focus();
        }

        private void InteractedEventHandler()
        {
            _interactable?.Interact(_holder);
        }
    
        private void UseEventHandler()
        {
            if (_interactable is not IUsable usable) return;
            usable.Use();
        }

        private void MovingEventHandler(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero) return;
            _castDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        }
    }
}