using System;
using System.Collections.Generic;
using Ingredient;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [Serializable]
    private struct GameObjectMap
    {
        public IngredientSO ingredientSO;
        public GameObject prefab;
    }
    
    [SerializeField] private Plate plate;
    [SerializeField] private List<GameObjectMap> ingredientPrefabs;

    private void Start()
    {
        plate.IngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(IngredientSO ingredientSO)
    {
        ingredientPrefabs.Find(map => map.ingredientSO == ingredientSO).prefab.SetActive(true);
    }
}