using KitchenObject;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutting Recipe", menuName = "Cutting Recipe", order = 0)]
public class CuttingRecipeSo : ScriptableObject
{
        public KitchenObjectSo input;
        public KitchenObjectSo output;
}