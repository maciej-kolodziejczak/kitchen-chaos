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
                var player = playerInteractions.GetComponent<PlayerKitchenObjectInteraction>();

                if (player.HasAttachedKitchenObject())
                {
                    // Player has something in hand
                    
                    if (HasAttachedKitchenObject())
                    {
                        // Counter has something on it, nothing to do
                        return;
                    };
                    
                    // Counter is empty, player puts the object on the counter
                    AttachKitchenObject(player.GetAttachedKitchenObject());
                    player.DetachKitchenObject();
                    
                    return;
                }

                // Player has nothing in hand
                if (!HasAttachedKitchenObject())
                {
                    // Nothing on the counter, nothing to do
                    return;
                }

                // Player has nothing in hand, trying to grab from counter
                player.AttachKitchenObject(GetAttachedKitchenObject());
                DetachKitchenObject();
        }
    }
}
