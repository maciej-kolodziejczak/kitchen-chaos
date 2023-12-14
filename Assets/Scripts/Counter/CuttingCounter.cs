using Player;

namespace Counter
{
    public class CuttingCounter : BaseCounter
    {
        public override void Interact(IKitchenObjectInteractor invoker)
        {

            if (invoker.HasAttachedKitchenObject())
            {
                // Player has something in hand
                    
                if (Interactor.HasAttachedKitchenObject())
                {
                    // Counter has something on it, swap objects
                    var playerKitchenObject = invoker.GetAttachedKitchenObject();
                    var counterKitchenObject = Interactor.GetAttachedKitchenObject();
                    
                    Interactor.AttachKitchenObject(playerKitchenObject);
                    invoker.AttachKitchenObject(counterKitchenObject);
                    
                    return;
                };
                    
                // Counter is empty, player puts the object on the counter
                Interactor.AttachKitchenObject(invoker.GetAttachedKitchenObject());
                invoker.DetachKitchenObject();
                    
                return;
            }

            // Player has nothing in hand
            if (!Interactor.HasAttachedKitchenObject())
            {
                // Nothing on the counter, nothing to do
                return;
            }

            // Player has nothing in hand, trying to grab from counter
            invoker.AttachKitchenObject(Interactor.GetAttachedKitchenObject());
            Interactor.DetachKitchenObject();
        }

        public override void InteractAlt(IKitchenObjectInteractor invoker)
        {
            if (!Interactor.HasAttachedKitchenObject())
            {
                return;
            }
            
            // destroy currently handled object
            Destroy(Interactor.GetAttachedKitchenObject().gameObject);
            Interactor.DetachKitchenObject();
        }
    }
}