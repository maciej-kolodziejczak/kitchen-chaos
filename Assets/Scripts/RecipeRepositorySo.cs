using System.Collections.Generic;
using KitchenObject;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class RecipeMap
{
    public KitchenObjectSo input;
    public KitchenObjectSo output;
    public float duration;
}

[CreateAssetMenu(fileName = "Recipe Repository", menuName = "Recipe Repository", order = 0)]
public class RecipeRepositorySo : ScriptableObject
{
    [SerializeField] public RecipeMap[] recipeMaps;
}