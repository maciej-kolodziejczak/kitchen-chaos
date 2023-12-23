using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlateIconTemplateUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}