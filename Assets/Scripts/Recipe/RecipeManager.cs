using UnityEngine;

namespace Recipe
{
    public class RecipeManager : MonoBehaviour
    {
        [SerializeField] private RecipeRepositorySO recipeRepositorySO;
        
        public static RecipeManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }
    }
}