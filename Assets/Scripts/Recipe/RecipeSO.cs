using Ingredient;
using UnityEngine;

namespace Recipe
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe", order = 0)]
    public class RecipeSO : ScriptableObject
    {
        public IngredientSO[] ingredients;
    }
}