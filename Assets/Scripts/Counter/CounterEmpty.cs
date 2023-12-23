using Common;

namespace Counter
{
    public class CounterEmpty : CounterBase
    {
        public override void Interact(IHolder invoker)
        {
            if (!Holder.IsHolding)
            {
                if (!invoker.IsHolding) return;
                
                Holder.Attach(invoker.AttachedHoldable);
                invoker.Detach();
                return;
            }
            
            if (!invoker.IsHolding)
            {
                invoker.Attach(Holder.AttachedHoldable);
                Holder.Detach();
                return;
            }

            var currentHoldable = Holder.AttachedHoldable;
            var invokerHoldable = invoker.AttachedHoldable;
            
            switch (currentHoldable)
            {
                case Plate plate when invokerHoldable is Ingredient.Ingredient ingredient:
                {
                    if (!plate.TryAddIngredient(ingredient.IngredientSO)) return;
                    invoker.Detach();
                    ingredient.Destroy();
                    return;
                }
                
                case Ingredient.Ingredient ingredient when invokerHoldable is Plate plate:
                {
                    if (!plate.TryAddIngredient(ingredient.IngredientSO)) return;
                    Holder.Detach();
                    ingredient.Destroy();
                    return;
                }
            }
        }
    }
}