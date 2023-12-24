using Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OrderTemplateUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private GameObject iconTemplate;

        public void SetOrderVisuals(RecipeSO order)
        {
            text.text = order.recipeName;
            foreach (var ingredient in order.ingredients)
            {
                var newIcon = Instantiate(iconTemplate, iconContainer);
                newIcon.SetActive(true);
                newIcon.GetComponent<Image>().sprite = ingredient.sprite;
            }
        }
    }
}