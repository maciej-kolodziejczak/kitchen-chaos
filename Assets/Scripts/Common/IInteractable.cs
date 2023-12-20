namespace Common
{
    public interface IInteractable<in T> : IFocusable
    {
        public void Interact(T interactor);
    }
}