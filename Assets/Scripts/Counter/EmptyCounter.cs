using Product;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProductHandler))]
    public class EmptyCounter : BaseCounter
    {
        private ProductHandler _productHandler;

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

                return;
            }
            
            if (_productHandler.HasProduct) return;
            
            _productHandler.PickUpProduct(invoker.Product);
            invoker.DropProduct();
        }
    }
}