using UnityEngine;

namespace Ingredient
{
        [CreateAssetMenu (fileName = "Ingredient Cookable", menuName = "Scriptable Objects/Ingredient Cookable", order = 1)]
        public class IngredientCookableSO : IngredientSO
        {
                public IngredientSO cookedResult;
                public float cookTime;
        }
}