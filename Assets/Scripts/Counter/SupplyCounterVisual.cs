using System;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(Animator))]
    public class SupplyCounterVisual : MonoBehaviour
    {
        [SerializeField] private SupplyCounter parentCounter;

        private Animator _animator;
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            parentCounter.GrabbedKitchenObject += GrabbedKitchenObject;
        }

        private void GrabbedKitchenObject()
        {
            _animator.SetTrigger(OpenClose);
        }
    }
}