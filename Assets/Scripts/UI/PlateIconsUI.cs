using System;
using Ingredient;
using UnityEngine;

namespace UI
{
    public class PlateIconsUI : MonoBehaviour
    {
        [SerializeField] private Plate plate;
        [SerializeField] private GameObject iconTemplate;

        private void Start()
        {
            plate.IngredientAdded += OnIngredientAdded;
        }

        private void OnIngredientAdded(IngredientSO ingredientSO)
        { 
            var newIcon = Instantiate(iconTemplate, transform);
            
            newIcon.GetComponent<PlateIconTemplateUI>().SetIcon(ingredientSO.sprite);
            newIcon.SetActive(true);
        }
    }
}