using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField]
    private Player player;
    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PerformAnimation();
    }

    private void Update()
    {
        PerformAnimation();
    }
    
    private void PerformAnimation()
    {
        _animator.SetBool(IsWalking, player.IsWalking);
    }

}
