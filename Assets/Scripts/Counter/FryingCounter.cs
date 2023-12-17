using System.Linq;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(KitchenObjectSpawner))]
    public class FryingCounter : BaseCounter
    {
        [SerializeField] private RecipeRepositorySo recipeRepositorySo;
        private KitchenObjectSpawner _kitchenObjectSpawner;
        
        public override void Awake()
        {
            base.Awake();
            _kitchenObjectSpawner = GetComponent<KitchenObjectSpawner>();
        }
        
        public override void Interact(KitchenObjectInteractor invoker)
        {
            // If player has something in hand
            if (invoker.HasAttachedKitchenObject())
            {
                // If object is not fryable, do nothing
                if (!HasRecipe(invoker.GetAttachedKitchenObject().KitchenObjectSo))
                {
                    return;
                }
                
                // If counter is empty, player puts the object on the counter
                if (!Interactor.HasAttachedKitchenObject())
                {
                    Interactor.AttachKitchenObject(invoker.GetAttachedKitchenObject());
                    invoker.DetachKitchenObject();
                    return;    
                }
                
                // If counter has something on it, swap objects
                var playerKitchenObject = invoker.GetAttachedKitchenObject();
                var counterKitchenObject = Interactor.GetAttachedKitchenObject();
                
                Interactor.AttachKitchenObject(playerKitchenObject);
                invoker.AttachKitchenObject(counterKitchenObject);
            }
            
            // If player has nothing in hand
            if (!Interactor.HasAttachedKitchenObject())
            {
                // If counter is empty, do nothing
                return;
            }
            
            // If counter has something on it, player picks it up
            invoker.AttachKitchenObject(Interactor.GetAttachedKitchenObject());
            Interactor.DetachKitchenObject();
        }
        
        private bool HasRecipe(KitchenObjectSo input)
        {
            return GetRecipe(input) != null;
        }        

        private RecipeMap GetRecipe(KitchenObjectSo input)
        {
            return recipeRepositorySo.recipeMaps.FirstOrDefault(recipeMap => recipeMap.input == input);
        }
    }
}