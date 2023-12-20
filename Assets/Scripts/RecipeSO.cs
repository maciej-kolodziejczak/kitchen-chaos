using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe", order = 0)]
public class RecipeSO : ScriptableObject
{
    public IngredientSO[] ingredients;
}