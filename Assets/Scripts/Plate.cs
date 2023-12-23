using System.Collections.Generic;
using Common;
using Ingredient;
using Recipe;
using UnityEngine;

public class Plate : MonoBehaviour, IHoldable, IDestroyable
{
    private readonly HashSet<IngredientSO> _ingredients = new();
    
    public void HoldAt(IHolder holder)
    {
        var transform1 = transform;
        
        transform.SetParent(holder.HoldPoint);
        transform1.localPosition = Vector3.zero;
        transform1.localRotation = Quaternion.identity;
    }
    
    public bool TryAddIngredient(IngredientSO ingredientSO)
    {
        var availableIngredients = RecipeManager.Instance.GetAvailableIngredients();
        
        return availableIngredients.Contains(ingredientSO) && _ingredients.Add(ingredientSO);
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