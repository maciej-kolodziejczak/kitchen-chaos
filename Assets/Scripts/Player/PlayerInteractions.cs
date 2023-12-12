using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField]
        private float interactionDistance = 1f;
    
        private PlayerInputHandler _playerInputHandler;
        private Vector3 _facingDirection;
    
        // @todo refactor when multiple interaction elements are added
        private EmptyCounter _selectedCounter;
    
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
                _selectedCounter = null;
                return;
            }
        
            _selectedCounter = hit.transform.TryGetComponent(out EmptyCounter emptyCounter) ? emptyCounter : null;
        }
    
        private void PlayerInputOnInteract(InputAction.CallbackContext obj)
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact();
            }
        }
    }
}
