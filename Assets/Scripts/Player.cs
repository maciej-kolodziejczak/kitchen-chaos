using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private PlayerInput playerInput;
    
    public bool IsWalking { get; private set; }
    
    private void Update()
    {
        var inputVector = playerInput.GetNormalizedInputVector();
        
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        IsWalking = moveDirection != Vector3.zero;

        var currentPosition = transform.position;
        var canMove = !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * 2f, .7f, moveDirection,
            moveSpeed * Time.deltaTime);
        
        // rotation is applied before movement as we want to face movement direction regardless of the collision, which would constrain the moveDirection to a single axis
        ApplyRotation(moveDirection);

        // @todo refactor this 
        if (canMove)
        { 
            ApplyMove(moveDirection);
        }
        else
        {
            // cast to check if we can move on x axis
            var canMoveOnX = !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * 2f, .7f,
                new Vector3(moveDirection.x, 0, 0), moveSpeed * Time.deltaTime);
            // cast to check if we can move on z axis
            var canMoveOnZ = !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * 2f, .7f,
                new Vector3(0, 0, moveDirection.z), moveSpeed * Time.deltaTime);
            
            // if we can move on x axis, update inputVector accordingly
            if (canMoveOnX)
            {
                inputVector = new Vector2(inputVector.x, 0).normalized;
            } else if (canMoveOnZ)
            {
                inputVector = new Vector2(0, inputVector.y).normalized;
            }
            
            // update moveDirection accordingly
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            
            // if we can move on either axis, apply move
            if (canMoveOnX || canMoveOnZ)
            {
                ApplyMove(moveDirection);
            }
        }
    }

    private void ApplyMove(Vector3 moveDirection)
    {
        transform.position += moveDirection * (Time.deltaTime * moveSpeed);
    }

    private void ApplyRotation(Vector3 moveDirection)
    {
        if (moveDirection == Vector3.zero)
        {
            return;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
}
