using System;
using System.Collections.Generic;
using Common;
using Ingredient;
using Recipe;
using UnityEngine;

public class Plate : MonoBehaviour, IHoldable, IDestroyable
{
    [Serializable]
    private struct GameObjectMap
    {
        public IngredientSO ingredientSO;
        public GameObject prefab;
    }
    
    [SerializeField] private List<GameObjectMap> ingredientPrefabs;
    
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
        if (!RecipeManager.Instance.GetAvailableIngredients().Contains(ingredientSO)) return false;
        if (!_ingredients.Add(ingredientSO)) return false;
        
        var ingredientVisual = ingredientPrefabs.Find(map => map.ingredientSO == ingredientSO).prefab;
        ingredientVisual.SetActive(true);
        return true;
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