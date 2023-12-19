using UnityEngine;

namespace Product
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Product", order = 0)]
    public class ProductSO : ScriptableObject
    {
        public Sprite sprite;
        public GameObject prefab;
    }
}