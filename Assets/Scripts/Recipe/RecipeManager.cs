using System.Collections.Generic;
using System.Linq;
using Ingredient;
using UnityEngine;

namespace Recipe
{
    public class RecipeManager : MonoBehaviour
    {
        [SerializeField] private RecipeRepositorySO recipeRepositorySO;
        
        public static RecipeManager Instance { get; private set; }

        public List<IngredientSO> GetAvailableIngredients()
        {
            return new HashSet<IngredientSO>(recipeRepositorySO.recipes.SelectMany(recipe => recipe.ingredients)).ToList();
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }
    }
}