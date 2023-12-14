using System.Linq;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(KitchenObjectSpawner))]
    public class CuttingCounter : BaseCounter
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
                }

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

        public override void InteractAlt(KitchenObjectInteractor invoker)
        {
            if (!Interactor.HasAttachedKitchenObject())
            {
                return;
            }
            
            
            var currentObject = Interactor.GetAttachedKitchenObject();
            var recipeResult = GetRecipeResult(currentObject.KitchenObjectSo);
            
            Debug.Log(currentObject);
            
            if (recipeResult == null)
            {
                return;
            }
            
            // destroy currently handled object
            currentObject.DestroySelf();
            Interactor.DetachKitchenObject();
            
            // spawn new object
            Interactor.AttachKitchenObject(
                _kitchenObjectSpawner.SpawnKitchenObject(recipeResult, Interactor.GetKitchenObjectOrigin()));
        }

        private KitchenObjectSo GetRecipeResult(KitchenObjectSo input)
        {
            return (from recipeMap in recipeRepositorySo.recipeMaps where recipeMap.input == input select recipeMap.output).FirstOrDefault();
        }
    }
}
