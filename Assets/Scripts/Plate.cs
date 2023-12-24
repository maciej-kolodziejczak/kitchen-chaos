using System;
using System.Collections.Generic;
using Common;
using Ingredient;
using Recipe;
using UnityEngine;

public class Plate : MonoBehaviour, IHoldable, IDestroyable
{
    public event Action<IngredientSO> IngredientAdded;
    public readonly HashSet<IngredientSO> Ingredients = new();
    
    public bool TryAddIngredient(IngredientSO ingredientSO)
    {
        if (!RecipeManager.Instance.GetAvailableIngredients().Contains(ingredientSO)) return false;
        if (!Ingredients.Add(ingredientSO)) return false;
        
        IngredientAdded?.Invoke(ingredientSO);
        
        return true;
    }

    public void HoldAt(IHolder holder)
    {
        var transform1 = transform;
        
        transform.SetParent(holder.HoldPoint);
        transform1.localPosition = Vector3.zero;
        transform1.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        transform.SetParent(null);
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}