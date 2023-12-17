using KitchenObject;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(KitchenObjectSpawner))]
    public class FryingCounter : BaseCounter
    {
        [SerializeField] private RecipeRepositorySo recipeRepositorySo;
        private KitchenObjectSpawner _kitchenObjectSpawner;
        
        public override void Awake()
        {
            base.Awake();
            _kitchenObjectSpawner = GetComponent<KitchenObjectSpawner>();
        }
        
        public override void Interact(KitchenObjectInteractor invoker)
        {
            Debug.Log("FryingCounter Interact");
        }
    }
}