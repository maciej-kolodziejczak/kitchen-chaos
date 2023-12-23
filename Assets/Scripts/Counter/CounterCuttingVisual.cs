using System;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(Animator))]
    public class CounterCuttingVisual : MonoBehaviour
    {
        [SerializeField] private CounterCutting counter;
        
        private Animator _animator;
        private static readonly int Cut = Animator.StringToHash("Cut");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            counter.Cut += OnCut;
        }

        private void OnCut()
        {
            _animator.SetTrigger(Cut);
        }
    }
}