using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private PlayerInputHandler _playerInputHandler;
    
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInputHandler = GetComponent<PlayerInputHandler>();
        }

        private void Start()
        {
            PerformAnimation();
        }

        private void Update()
        {
            PerformAnimation();
        }
    
        private void PerformAnimation()
        {
            var isWalking = _playerInputHandler.GetInputVector() != Vector2.zero;
            _animator.SetBool(IsWalking, isWalking);
        }

    }
}
