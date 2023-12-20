using System.Collections;
using Common;
using Ingredient;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProgressTracker))]
    public class CounterStove : CounterBase
    {
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

        public override void Interact(IHolder invoker)
        {
            if (!invoker.IsHolding)
            {
                if (!Holder.IsHolding) return;
            
                invoker.Attach(Holder.AttachedHoldable);
                Holder.Detach();
            
                StopCoroutine(Cook());
                _progressTracker.ResetProgress();

                return;
            }
        
            if (Holder.IsHolding) return;
            if (invoker.AttachedHoldable is not Ingredient.Ingredient ingredient) return;
            if (ingredient.IngredientSO is not IngredientCookableSO cookableSO) return;
        
            Holder.Attach(ingredient);
            invoker.Detach();

            StartCoroutine(Cook());
        }
    }
}