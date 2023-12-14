using System;
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
        
        // @todo fix this asap
        private float _currentProgress;
        public event Action<float> ProgressChanged;
        
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
                UpdateProgress(0f);
                    
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
            UpdateProgress(0f);
        }

        public override void InteractAlt(KitchenObjectInteractor invoker)
        {
            if (!Interactor.HasAttachedKitchenObject())
            {
                return;
            }
            
            var currentObject = Interactor.GetAttachedKitchenObject();
            var recipe = GetRecipe(currentObject.KitchenObjectSo);

            if (recipe == null)
            {
                return;
            }
            
            if (_currentProgress < recipe.duration - 1f)
            {
                UpdateProgress(_currentProgress + 1f, recipe.duration);
                return;
            }

            UpdateProgress(0f);
            
            // destroy currently handled object
            currentObject.DestroySelf();
            Interactor.DetachKitchenObject();
            
            // spawn new object
            Interactor.AttachKitchenObject(
                _kitchenObjectSpawner.SpawnKitchenObject(recipe?.output, Interactor.GetKitchenObjectOrigin()));
        }

        private void UpdateProgress(float progress, float max = 1)
        {
            _currentProgress = progress;
            ProgressChanged?.Invoke(progress / max);
        }

        private RecipeMap GetRecipe(KitchenObjectSo input)
        {
            return recipeRepositorySo.recipeMaps.FirstOrDefault(recipeMap => recipeMap.input == input);
        }
    }
}
