using System;
using Product;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(CounterProgress))]
    [RequireComponent(typeof(ProductHandler))]
    public class CuttingCounter : BaseCounter
    { 
        [SerializeField] private RecipesSO recipesSO;
        
        private ProductHandler _productHandler;
        private CounterProgress _counterProgress;

        private void Awake()
        {
            _productHandler = GetComponent<ProductHandler>();
            _counterProgress = GetComponent<CounterProgress>();
        }
        
        public override void Interact(ProductHandler invoker)
        {
            if (!invoker.HasProduct)
            {
                if (!_productHandler.HasProduct) return;

                invoker.PickUpProduct(_productHandler.Product);
                _productHandler.DropProduct();
                
                _counterProgress.ResetProgress();

                return;
            }
            
            if (_productHandler.HasProduct) return;
            
            _productHandler.PickUpProduct(invoker.Product);
            invoker.DropProduct();
            
            _counterProgress.ResetProgress();
        }
        
        public override void Use()
        {
            if (!_productHandler.HasProduct) return;
            
            var product = _productHandler.Product;
            var recipe = recipesSO.GetOutput(product.ProductSO);
            
            if (recipe == null) return;
            
            var maxProgress = recipesSO.GetDuration(product.ProductSO);
            
            if (!_counterProgress.HasStarted)
            {
                _counterProgress.StartProgress(maxProgress);
            }

            if (_counterProgress.IsInProgress)
            {
                _counterProgress.Progress(1);
            }
            
            if (!_counterProgress.IsFinished) return;

            var newProduct = Instantiate(recipe.prefab, _productHandler.ProductOrigin).GetComponent<Product.Product>();
            newProduct.SetOrigin(_productHandler.ProductOrigin);
            
            product.Destroy();
            _productHandler.PickUpProduct(newProduct);
            
            _counterProgress.ResetProgress();
        }
        
    }
}