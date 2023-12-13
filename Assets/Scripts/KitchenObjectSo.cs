using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSo : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public Sprite sprite;
    [SerializeField] public string objectName;
}
