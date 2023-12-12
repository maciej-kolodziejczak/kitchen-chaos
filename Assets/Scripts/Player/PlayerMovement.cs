using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float turnSpeed = 1f;

        private const float PlayerHeight = 2f; // @todo consider reading the value
        private const float PlayerRadius = .7f; // @todo consider reading the value
        private PlayerInputHandler _playerInputHandler;
    
        private void Start()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();    
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var inputVector = _playerInputHandler.GetNormalizedInputVector();
            var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        
            var currentPosition = transform.position;
            var canMove = CanMove(currentPosition, moveDirection);

            ApplyRotation(moveDirection);

            if (canMove)
            {
                ApplyMove(moveDirection);
            }
            else
            {
                HandleCollisionMove(inputVector, currentPosition);
            }
        }
    
        private void HandleCollisionMove(Vector2 inputVector, Vector3 currentPosition)
        {
            var canMoveOnX = !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * 2f, .7f, new Vector3(inputVector.x, 0, 0), moveSpeed * Time.deltaTime);
            var canMoveOnZ = !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * 2f, .7f, new Vector3(0, 0, inputVector.y), moveSpeed * Time.deltaTime);

            if (canMoveOnX)
            {
                inputVector = new Vector2(inputVector.x, 0).normalized;
            }
            else if (canMoveOnZ)
            {
                inputVector = new Vector2(0, inputVector.y).normalized;
            }


            if (!canMoveOnX && !canMoveOnZ) return;
        
            var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        
            ApplyMove(moveDirection);
        }

        private void ApplyRotation(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
            {
                return;
            }
        
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, turnSpeed * Time.deltaTime);
        }
    
        private void ApplyMove(Vector3 moveDirection)
        {
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        private bool CanMove(Vector3 currentPosition, Vector3 moveDirection)
        {
            var maxDistance = moveSpeed * Time.deltaTime;
        
            return !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * PlayerHeight, PlayerRadius, moveDirection, maxDistance);
        }
    }
}
