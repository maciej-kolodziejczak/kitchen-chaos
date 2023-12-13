using System;
using Player;
using UnityEngine;

namespace Counter
{
    public class SupplyCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
        
        public event Action GrabbedKitchenObject;
    
        public override void Interact(PlayerInteractions playerInteractions)
        {
            var kitchenObject = Instantiate(kitchenObjectSo.prefab, prefabSpawnGameObject.transform);
            kitchenObject.GetComponent<KitchenObject>().AttachToParent(playerInteractions.GetComponent<IKitchenObjectParent>());
            GrabbedKitchenObject?.Invoke();
        }
    }
}
