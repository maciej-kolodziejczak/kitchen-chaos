using UnityEngine;

namespace KitchenObject
{
    [CreateAssetMenu(fileName = "Kitchen Object", menuName = "Kitchen Object", order = 0)]
    public class KitchenObjectSo : ScriptableObject
    {
        [SerializeField] public GameObject prefab;
        [SerializeField] public GameObject visualPrefab;
        [SerializeField] public Sprite sprite;
        [SerializeField] public string objectName;
    }
}
