using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingredient;
using UnityEngine;
using Random = System.Random;

namespace Recipe
{
    public class RecipeManager : MonoBehaviour
    {
        [SerializeField] private List<RecipeSO> recipes;
        
        public List<RecipeSO> Recipes => recipes;
        public static RecipeManager Instance { get; private set; }

        public List<IngredientSO> GetAvailableIngredients()
        {
            return new HashSet<IngredientSO>(recipes.SelectMany(recipe => recipe.ingredients)).ToList();
        }

        public bool TryGetMatchingRecipe(List<IngredientSO> ingredients, out RecipeSO recipe)
        { 
            recipe = recipes.FirstOrDefault(recipe => 
                recipe.ingredients.Count == ingredients.Count && 
                recipe.ingredients.All(ingredients.Contains));
            
            return recipe != null;
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }
    }
}