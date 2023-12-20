public class CounterEmpty : CounterBase
{
    public override void Interact(IHolder invoker)
    {
        if (!invoker.IsHolding)
        {
            if (!Holder.IsHolding) return;
            
            invoker.Attach(Holder.AttachedHoldable);
            Holder.Detach();

            return;
        }

        if (Holder.IsHolding) return;
        
        Holder.Attach(invoker.AttachedHoldable);
        invoker.Detach();
    }
}