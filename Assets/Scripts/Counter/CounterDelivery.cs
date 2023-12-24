using System.Linq;
using Common;
using Recipe;
using UnityEngine;

namespace Counter
{
    public class CounterDelivery : CounterBase
    {
        public override void Interact(IHolder invoker)
        {
            if (!invoker.IsHolding || invoker.AttachedHoldable is not Plate plate) return;
            if (!RecipeManager.Instance.TryGetMatchingRecipe(plate.Ingredients.ToList(), out var recipe)) return;

            switch (OrderManager.Instance.TryFulfillOrder(recipe))
            {
                case true:
                    Debug.Log("Recipe delivered");
                    break;
                case false:
                    Debug.Log("Recipe not delivered");
                    break;
            }
            
            plate.Destroy();
            invoker.Detach();
        }
    }
}
