using System.Collections;
using Product;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProductHandler))]
    [RequireComponent(typeof(ProgressTracker))]
    public class StoveCounter : BaseCounter
    {
        [SerializeField] private RecipesSO recipesSO;
        [SerializeField] private GameObject heatingEffect;
        [SerializeField] private GameObject particlesEffect;
        
        private ProductHandler _productHandler;
        private ProgressTracker progressTracker;

        private void Awake()
        {
            _productHandler = GetComponent<ProductHandler>();
            progressTracker = GetComponent<ProgressTracker>();
        }

        private IEnumerator RunProgress()
        {
            const float interval = .1f;
            
            while (_productHandler.HasProduct)
            {
                var product = _productHandler.Product;
                var input = product.ProductSO;
                
                if (!recipesSO.HasRecipe(input)) yield break;
                
                if (!progressTracker.HasStarted)
                {
                    progressTracker.StartProgress(recipesSO.GetDuration(input));
                }
                
                if (progressTracker.IsInProgress)
                {
                    progressTracker.Progress(interval);
                }
                
                if (progressTracker.IsFinished)
                {
                    
                    var recipe = recipesSO.GetOutput(input);
                    var newProduct = Instantiate(recipe.prefab, _productHandler.ProductOrigin)
                        .GetComponent<Product.Product>();
                    
                    product.Destroy();
                    _productHandler.PickUpProduct(newProduct);
                    
                    progressTracker.ResetProgress();

                    yield return null;
                }
                
                yield return new WaitForSeconds(interval);
            }
        }

        public override void Interact(ProductHandler invoker)
        {
            if (!invoker.HasProduct)
            {
                if (!_productHandler.HasProduct) return;

                invoker.PickUpProduct(_productHandler.Product);
                _productHandler.DropProduct();
                
                StopCoroutine(RunProgress());
                heatingEffect.SetActive(false);
                particlesEffect.SetActive(false);
                progressTracker.ResetProgress();

                return;
            }
            
            if (_productHandler.HasProduct) return;
            
            if (!recipesSO.HasRecipe(invoker.ProductSO)) return;
            
            _productHandler.PickUpProduct(invoker.Product);
            invoker.DropProduct();

            StartCoroutine(RunProgress());
            heatingEffect.SetActive(true);
            particlesEffect.SetActive(true);
            progressTracker.ResetProgress();
        }
    }
}