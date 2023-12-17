using KitchenObject;

namespace Counter
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(KitchenObjectInteractor invoker)
        {
            if (!invoker.HasAttachedKitchenObject()) return;
            
            invoker.GetAttachedKitchenObject().DestroySelf();
            invoker.DetachKitchenObject();
        }
    }
}
