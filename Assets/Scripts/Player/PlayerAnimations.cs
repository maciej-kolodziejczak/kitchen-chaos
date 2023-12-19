using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
    
        private PlayerInputHandler _inputHandler;
    
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();
            _inputHandler.Moving += MovingEventHandler;
        }

        private void MovingEventHandler(Vector2 movementInput)
        {
            animator.SetBool(IsMoving, movementInput != Vector2.zero);
        }
    }
}