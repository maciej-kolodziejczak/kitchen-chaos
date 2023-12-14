using System;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    public class SupplyCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
        
        public event Action GrabbedKitchenObject;
    
        public override void Interact(KitchenObjectInteractor invoker)
        {
            
            if (invoker.HasAttachedKitchenObject())
            {
                return;
            }
            
            var kitchenObject = Instantiate(kitchenObjectSo.prefab, Interactor.GetKitchenObjectOrigin());
            
            invoker.AttachKitchenObject(kitchenObject.GetComponent<KitchenObject.KitchenObject>());
            GrabbedKitchenObject?.Invoke();
        }
    }
}
