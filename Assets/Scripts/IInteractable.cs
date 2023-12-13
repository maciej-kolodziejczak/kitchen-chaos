using Player;

public interface IInteractable<in T>
{
    public void Interact(T invoker);
}