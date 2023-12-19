using UnityEngine;

namespace Product
{
    public class ProductHandler : MonoBehaviour
    {
        [SerializeField] private Transform productOrigin;
        
        public Product Product { get; private set; }
        public ProductSO ProductSO => Product.ProductSO;
        public bool HasProduct => Product != null;
        public Transform ProductOrigin => productOrigin;

        public void PickUpProduct(Product product)
        {
            Product = product;
            Product.SetOrigin(productOrigin);
        }

        public void DropProduct()
        {
            Product = null;
        }
    }
}