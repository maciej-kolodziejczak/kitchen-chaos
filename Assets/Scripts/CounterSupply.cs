using UnityEngine;

public class CounterSupply : CounterBase
{
     [SerializeField] private IngredientSO ingredientSO;
     [SerializeField] private Animator animator;
     
     private static readonly int OpenClose = Animator.StringToHash("OpenClose");

     public override void Interact(IHolder invoker)
     {
          if (invoker.IsHolding) return;

          var newGameObject =
               Instantiate(ingredientSO.prefab, invoker.HoldPoint);
          
          invoker.Attach(newGameObject.GetComponent<Ingredient>());
          animator.SetTrigger(OpenClose);
     }
}