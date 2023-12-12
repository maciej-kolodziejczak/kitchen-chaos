using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float rotationSpeed = 1f;
    
    public bool IsWalking { get; private set; }
    
    private void Update()
    {
        var inputVector = Vector2.zero;
        inputVector = ReadInputVector(inputVector);
        
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        IsWalking = moveDirection != Vector3.zero;
        
        ApplyMove(inputVector, moveDirection);
        ApplyRotation(moveDirection);
    }

    private void ApplyMove(Vector2 inputVector, Vector3 moveDirection)
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

    private Vector2 ReadInputVector(Vector2 inputVector)
    {
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        
        inputVector.Normalize();

        return inputVector;
    }
}
