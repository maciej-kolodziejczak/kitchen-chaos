using System;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(KitchenObjectSpawner))]
    public class SupplyCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
        
        private KitchenObjectSpawner _kitchenObjectSpawner;
        
        public event Action GrabbedKitchenObject;
        
        public override void Awake()
        {
            base.Awake();
            _kitchenObjectSpawner = GetComponent<KitchenObjectSpawner>();
        }
        
    
        public override void Interact(KitchenObjectInteractor invoker)
        {
            
            if (invoker.HasAttachedKitchenObject())
            {
                return;
            }
            
            invoker.AttachKitchenObject(_kitchenObjectSpawner.SpawnKitchenObject(kitchenObjectSo, Interactor.GetKitchenObjectOrigin()));
            GrabbedKitchenObject?.Invoke();
        }
    }
}
