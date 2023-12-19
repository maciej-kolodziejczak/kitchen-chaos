using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationSpeed = 5f;
    
        private PlayerInputHandler _inputHandler;
        
        private const float PlayerHeight = 2f;
        private const float PlayerRadius = .7f;
    
        private void Awake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();
            _inputHandler.Moving += MovingEventHandler;
        }
    
        private void OnDestroy()
        {
            _inputHandler.Moving -= MovingEventHandler;
        }

        private void MovingEventHandler(Vector2 movementInput)
        {
            PerformMovement(movementInput);
            PerformRotation(movementInput);
        }

        private void PerformRotation(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero) return;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.y)), rotationSpeed * Time.deltaTime);
        }

        private void PerformMovement(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero) return;
            
            var moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
            var canMove = !IsColliding(moveDirection);

            if (!canMove)
            {
                var canMoveOnX = !IsColliding(new Vector3(movementInput.x, 0, 0));
                var canMoveOnZ = !IsColliding(new Vector3(0, 0, movementInput.y));
                
                if (!canMoveOnX && !canMoveOnZ) return;

                moveDirection = canMoveOnX ? new Vector3(movementInput.x, 0, 0) : new Vector3(0, 0, movementInput.y);
                moveDirection.Normalize();
            }
            
            transform.position += moveDirection * (movementSpeed * Time.deltaTime);
        }

        private bool IsColliding(Vector3 direction)
        {
            var position = transform.position;
            
            return Physics.CapsuleCast(position, position + Vector3.up * PlayerHeight,
                PlayerRadius, direction, movementSpeed * Time.deltaTime);
        }
    }
}
