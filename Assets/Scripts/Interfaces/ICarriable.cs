namespace Interfaces
{
    public interface ICarriable : IInteractable
    {
        public bool PickUp();
        public void Drop();
    }
}