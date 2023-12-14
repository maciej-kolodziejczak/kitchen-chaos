using System.Collections.Generic;
using KitchenObject;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct RecipeMap
{
    public KitchenObjectSo input;
    public KitchenObjectSo output;
}

[CreateAssetMenu(fileName = "Recipe Repository", menuName = "Recipe Repository", order = 0)]
public class RecipeRepositorySo : ScriptableObject
{
    [SerializeField] public RecipeMap[] recipeMaps;
}