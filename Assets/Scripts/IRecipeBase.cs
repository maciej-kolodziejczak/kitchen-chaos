public interface IRecipeBase
{
        public IIngredient[] Ingredients { get; }
        public void AddIngredient(IIngredient ingredient) {}
}