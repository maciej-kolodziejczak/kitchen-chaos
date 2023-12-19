using Product;

namespace Counter
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(ProductHandler invoker)
        {
            if (!invoker.HasProduct) return;
            
            invoker.Product.Destroy();
            invoker.DropProduct();
        }
    }
}