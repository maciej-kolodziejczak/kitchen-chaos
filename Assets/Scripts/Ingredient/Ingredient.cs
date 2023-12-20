using Common;
using UnityEngine;

namespace Ingredient
{
    public class Ingredient : MonoBehaviour, IIngredient, IHoldable, IDestroyable
    {
        [SerializeField] private IngredientSO ingredientSO;
    
        public IngredientSO IngredientSO => ingredientSO;
    
        public void HoldAt(IHolder holder)
        {
            var transform1 = transform;
        
            transform.SetParent(holder.HoldPoint);
            transform1.localPosition = Vector3.zero;
            transform1.localRotation = Quaternion.identity;
        }
    
        public void Release()
        {
            transform.SetParent(null);
        }
    
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}