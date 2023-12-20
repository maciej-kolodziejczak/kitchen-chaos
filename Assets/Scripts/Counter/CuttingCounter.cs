using System;
using Product;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProgressTracker))]
    [RequireComponent(typeof(ProductHandler))]
    public class CuttingCounter : BaseCounter
    { 
        [SerializeField] private RecipesSO recipesSO;
        
        private ProductHandler _productHandler;
        private ProgressTracker progressTracker;

        private void Awake()
        {
            _productHandler = GetComponent<ProductHandler>();
            progressTracker = GetComponent<ProgressTracker>();
        }
        
        public override void Interact(ProductHandler invoker)
        {
            if (!invoker.HasProduct)
            {
                if (!_productHandler.HasProduct) return;

                invoker.PickUpProduct(_productHandler.Product);
                _productHandler.DropProduct();
                
                progressTracker.ResetProgress();

                return;
            }
            
            if (_productHandler.HasProduct) return;
            
            _productHandler.PickUpProduct(invoker.Product);
            invoker.DropProduct();
            
            progressTracker.ResetProgress();
        }
        
        public override void Use()
        {
            if (!_productHandler.HasProduct) return;
            
            var product = _productHandler.Product;
            var recipe = recipesSO.GetOutput(product.ProductSO);
            
            if (recipe == null) return;
            
            var maxProgress = recipesSO.GetDuration(product.ProductSO);
            
            if (!progressTracker.HasStarted)
            {
                progressTracker.StartProgress(maxProgress);
            }

            if (progressTracker.IsInProgress)
            {
                progressTracker.Progress(1);
            }
            
            if (!progressTracker.IsFinished) return;

            var newProduct = Instantiate(recipe.prefab, _productHandler.ProductOrigin).GetComponent<Product.Product>();
            newProduct.SetOrigin(_productHandler.ProductOrigin);
            
            product.Destroy();
            _productHandler.PickUpProduct(newProduct);
            
            progressTracker.ResetProgress();
        }
        
    }
}