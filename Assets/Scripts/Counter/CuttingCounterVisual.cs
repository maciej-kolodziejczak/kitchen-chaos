using System;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(Animator))]
    public class CuttingCounterVisual : MonoBehaviour
    {
        [SerializeField] private CuttingCounter parentCounter;
        
        private Animator _animator;
        private static readonly int Cut = Animator.StringToHash("Cut");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            parentCounter.CuttingKitchenObject += OnCuttingKitchenObject;
        }
        
        private void OnCuttingKitchenObject()
        {
            _animator.SetTrigger(Cut);
        }
    }
}