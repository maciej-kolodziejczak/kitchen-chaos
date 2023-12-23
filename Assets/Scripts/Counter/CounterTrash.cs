using Common;

namespace Counter
{
    public class CounterTrash : CounterBase
    {
        public override void Interact(IHolder invoker)
        {
            if (!invoker.IsHolding) return;
            if (invoker.AttachedHoldable is not IDestroyable destroyable) return;
        
            destroyable.Destroy();
            invoker.Detach();
        }
    }
}