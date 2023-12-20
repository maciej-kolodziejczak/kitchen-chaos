using Counter;
using UnityEngine;

[RequireComponent(typeof(ProgressTracker))]
public class CounterCutting : CounterBase, ICutter
{
    private ProgressTracker _progressTracker;

    protected override void Awake()
    {
        base.Awake();
        _progressTracker = GetComponent<ProgressTracker>();
    }

    public override void Interact(IHolder invoker)
    {
        if (!invoker.IsHolding)
        {
            if (!Holder.IsHolding) return;
            
            invoker.Attach(Holder.AttachedHoldable);
            Holder.Detach();

            return;
        }
        
        if (Holder.IsHolding) return;
        if (invoker.AttachedHoldable is not Ingredient ingredient) return;
        if (ingredient.IngredientSO is not IngredientCuttableSO) return;
        
        Holder.Attach(invoker.AttachedHoldable);
        invoker.Detach();
    }

    public void Cut()
    {
        if (!Holder.IsHolding) return;
        if (Holder.AttachedHoldable is not Ingredient ingredient) return;
        Debug.Log(ingredient.IngredientSO);
        if (ingredient.IngredientSO is not IngredientCuttableSO cuttableSO) return;
        
        var cutCount = cuttableSO.cutCount;

        if (!_progressTracker.IsInProgress)
        {
            _progressTracker.StartProgress(cutCount);
        }
        
        _progressTracker.Progress(1);
        
        if (!_progressTracker.IsFinished) return;
        
        var cutResult = cuttableSO.cutResult;
        var newGameObject = Instantiate(cutResult.prefab, Holder.HoldPoint);
        
        ingredient.Destroy();
        Holder.Detach();
        Holder.Attach(newGameObject.GetComponent<Ingredient>());
    }
}