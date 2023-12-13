using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Counter
{
    public class EmptyCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSo kitchenObjectSo;
        
        public override void Interact(PlayerInteractions playerInteractions)
        {
            if (HasAttachedKitchenObject())
            {
                playerInteractions.GetComponent<PlayerKitchenObjectInteraction>().AttachKitchenObject(GetAttachedKitchenObject());
                DetachKitchenObject();
                return;
            }
            
            var kitchenObject = Instantiate(kitchenObjectSo.prefab, prefabSpawnGameObject.transform);
            AttachKitchenObject(kitchenObject.GetComponent<KitchenObject>());
        }

        
    }
}
