using System;
using Player;
using Product;
using UnityEngine;

namespace Counter
{
    public class SupplyCounter : BaseCounter
    {
        [SerializeField] private ProductSO productSO;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");

        private void Awake()
        {
            spriteRenderer.sprite = productSO.sprite;
        }

        public override void Interact(ProductHandler invoker)
        {
            if (invoker.HasProduct) return;

            var newProduct = Instantiate(productSO.prefab, invoker.ProductOrigin).GetComponent<Product.Product>();
            
            animator.SetTrigger(OpenClose);
            newProduct.SetOrigin(invoker.ProductOrigin);
            invoker.PickUpProduct(newProduct);
        }
    }
}