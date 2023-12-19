using System;
using Product;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProductHandler))]
    public class CuttingCounter : BaseCounter
    { 
        [SerializeField] private RecipesSO recipesSO;
        
        public event Action<float> ProgressChanged;
        
        private ProductHandler _productHandler;

        private float _cuttingProgress; 

        private void Awake()
        {
            _productHandler = GetComponent<ProductHandler>();
        }
        
        public override void Interact(ProductHandler invoker)
        {
            if (!invoker.HasProduct)
            {
                if (!_productHandler.HasProduct) return;

                invoker.PickUpProduct(_productHandler.Product);
                _productHandler.DropProduct();
                
                _cuttingProgress = 0;
                ProgressChanged?.Invoke(0);

                return;
            }
            
            if (_productHandler.HasProduct) return;
            
            _productHandler.PickUpProduct(invoker.Product);
            invoker.DropProduct();
            
            _cuttingProgress = 0;
            ProgressChanged?.Invoke(0);
        }
        
        public override void Use()
        {
            if (!_productHandler.HasProduct) return;
            
            var product = _productHandler.Product;
            var recipe = recipesSO.GetOutput(product.ProductSO);
            
            if (recipe == null) return;
            
            var maxProgress = recipesSO.GetDuration(product.ProductSO);

            if (_cuttingProgress < maxProgress - 1)
            {
                _cuttingProgress++;
                ProgressChanged?.Invoke(_cuttingProgress / maxProgress);
                return;
            }

            var newProduct = Instantiate(recipe.prefab, _productHandler.ProductOrigin).GetComponent<Product.Product>();
            newProduct.SetOrigin(_productHandler.ProductOrigin);
            
            product.Destroy();
            _productHandler.PickUpProduct(newProduct);
            
            _cuttingProgress = 0;
            ProgressChanged?.Invoke(0);
        }
        
    }
}