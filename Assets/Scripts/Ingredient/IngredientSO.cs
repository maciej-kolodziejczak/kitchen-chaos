using UnityEngine;

namespace Ingredient
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient", order = 0)]
    public class IngredientSO : ScriptableObject
    {
        public Sprite sprite;
        public GameObject prefab;
    }
}