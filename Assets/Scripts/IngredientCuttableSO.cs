using UnityEngine;

[CreateAssetMenu (fileName = "Ingredient Cuttable", menuName = "Scriptable Objects/Ingredient Cuttable", order = 1)]
public class IngredientCuttableSO : IngredientSO
{
        public int cutCount;
        public IngredientSO cutResult;
}