using System;
using Common;
using Ingredient;
using UnityEngine;

namespace Counter
{
     public class CounterSupply : CounterBase
     {
          [SerializeField] private IngredientSO ingredientSO;
          
          public event Action Opened;

          public override void Interact(IHolder invoker)
          {
               if (invoker.IsHolding)
               {
                    if (invoker.AttachedHoldable is not Plate plate) return;
                    if (!plate.TryAddIngredient(ingredientSO)) return;
                    Opened?.Invoke();
                    return;
               }
               
               var newGameObject =
                    Instantiate(ingredientSO.prefab, invoker.HoldPoint);

               invoker.Attach(newGameObject.GetComponent<Ingredient.Ingredient>());
               Opened?.Invoke();
          }
     }
}