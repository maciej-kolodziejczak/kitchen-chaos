using System;
using System.Collections;
using Product;
using UnityEngine;
using UnityEngine.Serialization;

namespace Counter
{
    [RequireComponent(typeof(ProductHandler))]
    [RequireComponent(typeof(CounterProgress))]
    public class StoveCounter : BaseCounter
    {
        [SerializeField] private RecipesSO recipesSO;
        [SerializeField] private GameObject heatingEffect;
        [SerializeField] private GameObject particlesEffect;
        
        private ProductHandler _productHandler;
        private CounterProgress _counterProgress;

        private void Awake()
        {
            _productHandler = GetComponent<ProductHandler>();
            _counterProgress = GetComponent<CounterProgress>();
        }

        private IEnumerator RunProgress()
        {
            const float interval = .1f;
            
            while (_productHandler.HasProduct)
            {
                var product = _productHandler.Product;
                var input = product.ProductSO;
                
                if (!recipesSO.HasRecipe(input)) yield break;
                
                if (!_counterProgress.HasStarted)
                {
                    _counterProgress.StartProgress(recipesSO.GetDuration(input));
                }
                
                if (_counterProgress.IsInProgress)
                {
                    _counterProgress.Progress(interval);
                }
                
                if (_counterProgress.IsFinished)
                {
                    
                    var recipe = recipesSO.GetOutput(input);
                    var newProduct = Instantiate(recipe.prefab, _productHandler.ProductOrigin)
                        .GetComponent<Product.Product>();
                    
                    product.Destroy();
                    _productHandler.PickUpProduct(newProduct);
                    
                    _counterProgress.ResetProgress();

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
                _counterProgress.ResetProgress();

                return;
            }
            
            if (_productHandler.HasProduct) return;
            
            if (!recipesSO.HasRecipe(invoker.ProductSO)) return;
            
            _productHandler.PickUpProduct(invoker.Product);
            invoker.DropProduct();

            StartCoroutine(RunProgress());
            heatingEffect.SetActive(true);
            particlesEffect.SetActive(true);
            _counterProgress.ResetProgress();
        }
    }
}