using System;
using System.Linq;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(KitchenObjectSpawner))]
    [RequireComponent(typeof(StepProgressTracker))]
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private RecipeRepositorySo recipeRepositorySo;
        
        private KitchenObjectSpawner _kitchenObjectSpawner;
        private StepProgressTracker _stepProgressTracker;
        
        public event Action CuttingKitchenObject;
        
        public override void Awake()
        {
            base.Awake();
            _kitchenObjectSpawner = GetComponent<KitchenObjectSpawner>();
            _stepProgressTracker = GetComponent<StepProgressTracker>();
        }
        
        public override void Interact(KitchenObjectInteractor invoker)
        {
            // Player has something in hand
            if (invoker.HasAttachedKitchenObject())
            {
                // Counter has something on it, swap objects
                if (Interactor.HasAttachedKitchenObject())
                {
                    // If cutting is in progress, reset the progress
                    _stepProgressTracker.ResetProgress();
                    
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
            
            // If cutting is in progress, reset the progress
            _stepProgressTracker.ResetProgress();

            // Player has nothing in hand, trying to grab from counter
            invoker.AttachKitchenObject(Interactor.GetAttachedKitchenObject());
            Interactor.DetachKitchenObject();
        }

        public override void InteractAlt(KitchenObjectInteractor invoker)
        {
            // If there's nothing on the counter, nothing to do
            if (!Interactor.HasAttachedKitchenObject())
            {
                return;
            }
            
            // Check if object on the counter is cuttable
            var currentObject = Interactor.GetAttachedKitchenObject();
            var recipe = GetRecipe(currentObject.KitchenObjectSo);

            if (recipe == null)
            {
                return;
            }
            
            // If cutting is not in progress, start it
            if (!_stepProgressTracker.InProgress)
            {
                _stepProgressTracker.StartProgress(recipe.duration);
            }
            
            // Update cutting progress
            _stepProgressTracker.UpdateProgress();
            // Notify listeners that cutting is in progress
            CuttingKitchenObject?.Invoke();
            
            if (!_stepProgressTracker.IsCompleted)
            {
                return;
            }
            
            // If cutting is completed, spawn the output
            
            // destroy currently handled object
            currentObject.DestroySelf();
            Interactor.DetachKitchenObject();
            
            // spawn new object
            Interactor.AttachKitchenObject(
                _kitchenObjectSpawner.SpawnKitchenObject(recipe?.output, Interactor.GetKitchenObjectOrigin()));
        }

        private RecipeMap GetRecipe(KitchenObjectSo input)
        {
            return recipeRepositorySo.recipeMaps.FirstOrDefault(recipeMap => recipeMap.input == input);
        }
    }
}
