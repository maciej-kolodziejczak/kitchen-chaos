using System;
using KitchenObject;
using Player;
using UnityEngine;

namespace Counter
{
    public class SupplyCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
        
        public event Action GrabbedKitchenObject;
    
        public override void Interact(IKitchenObjectInteractor invoker)
        {
            
            if (invoker.HasAttachedKitchenObject())
            {
                return;
            }
            
            var kitchenObject = Instantiate(kitchenObjectSo.prefab, kitchenObjectOrigin);
            
            invoker.AttachKitchenObject(kitchenObject.GetComponent<KitchenObject.KitchenObject>());
            GrabbedKitchenObject?.Invoke();
        }
    }
}
