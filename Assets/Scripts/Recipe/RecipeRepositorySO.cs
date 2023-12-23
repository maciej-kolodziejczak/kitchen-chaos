using UnityEngine;

namespace Recipe
{
    [CreateAssetMenu(fileName = "Recipe Repository", menuName = "Scriptable Objects/Recipe Repository", order = 0)]
    public class RecipeRepositorySO : ScriptableObject
    {
        public RecipeSO[] recipes;
    }
}