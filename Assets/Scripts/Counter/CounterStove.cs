using System;
using System.Collections;
using Common;
using Ingredient;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProgressTracker))]
    public class CounterStove : CounterBase
    {
        public event Action CookingStarted;
        public event Action CookingStopped;

        private ProgressTracker _progressTracker;

        protected override void Awake()
        {
            base.Awake();
            _progressTracker = GetComponent<ProgressTracker>();
        }
    
        private IEnumerator Cook()
        {
            const float interval = .1f;
        
            while (Holder.AttachedHoldable is Ingredient.Ingredient ingredient)
            {
                if (ingredient.IngredientSO is not IngredientCookableSO cookableSO) yield break;
            
                if (!_progressTracker.IsInProgress)
                {
                    _progressTracker.StartProgress(cookableSO.cookTime);
                }
            
                _progressTracker.Progress(interval);

                if (!_progressTracker.IsFinished)
                {
                    yield return new WaitForSeconds(interval);
                    continue;
                };
            
                var cookedResult = cookableSO.cookedResult;
                var newGameObject = Instantiate(cookedResult.prefab, Holder.HoldPoint);
            
                ingredient.Destroy();
                Holder.Detach();
                Holder.Attach(newGameObject.GetComponent<Ingredient.Ingredient>());
            
                _progressTracker.ResetProgress();

                yield return null;
            }
        }

        private void StartCooking()
        {
            CookingStarted?.Invoke();
            StartCoroutine(Cook());
        }

        private void StopCooking()
        {
            StopCoroutine(Cook());
            _progressTracker.ResetProgress();
            CookingStopped?.Invoke();
        }

        public override void Interact(IHolder invoker)
        {
            if (!Holder.IsHolding)
            {
                if (!invoker.IsHolding) return;
                if (invoker.AttachedHoldable is not Ingredient.Ingredient { IngredientSO: IngredientCookableSO } cookable) return;
                
                Holder.Attach(cookable);
                invoker.Detach();
                
                StartCooking();
                return;
            }
            
            if (!invoker.IsHolding)
            {
                invoker.Attach(Holder.AttachedHoldable);
                Holder.Detach();
                
                StopCooking();
                return;
            }
            
            if (invoker.AttachedHoldable is not Plate plate) return;
            if (Holder.AttachedHoldable is not Ingredient.Ingredient ingredient) return;
            
            if (!plate.TryAddIngredient(ingredient.IngredientSO)) return;
            
            Holder.Detach();
            ingredient.Destroy();
            
            StopCooking();
        }
    }
}