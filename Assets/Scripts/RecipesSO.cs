using System;
using System.Linq;
using Product;
using UnityEngine;

[Serializable]
public struct Recipe
{
    public ProductSO input;
    public ProductSO output;
    public float duration;
}

[CreateAssetMenu(fileName = "Recipes", menuName = "Scriptable Objects/Recipes", order = 0)]
public class RecipesSO : ScriptableObject
{
    [SerializeField] private Recipe[] recipeRecords;
    
    public bool HasRecipe(ProductSO input)
    {
        return recipeRecords.Any(recipeRecord => recipeRecord.input == input);
    }

    public ProductSO GetOutput(ProductSO input)
    {
        return (from recipeRecord in recipeRecords where recipeRecord.input == input select recipeRecord.output)
            .FirstOrDefault();
    }

    public float GetDuration(ProductSO input)
    {
        return (from recipeRecord in recipeRecords where recipeRecord.input == input select recipeRecord.duration)
            .FirstOrDefault();
    }
}