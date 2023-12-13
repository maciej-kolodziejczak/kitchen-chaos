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
            var player = playerInteractions.GetComponent<IKitchenObjectParent>();
            
            if (player.HasAttachedKitchenObject())
            {
                return;
            }
            
            var kitchenObject = Instantiate(kitchenObjectSo.prefab, prefabSpawnGameObject.transform);
            
            player.AttachKitchenObject(kitchenObject.GetComponent<KitchenObject>());
            GrabbedKitchenObject?.Invoke();
        }
    }
}
