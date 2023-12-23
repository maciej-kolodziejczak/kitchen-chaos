using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(Animator))]
    public class CounterSupplyVisual : MonoBehaviour
    {
        [SerializeField] private CounterSupply counter;

        private Animator _animator;
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            counter.Opened += OnOpened;
        }

        private void OnOpened()
        {
            _animator.SetTrigger(OpenClose);
        }
    }
}