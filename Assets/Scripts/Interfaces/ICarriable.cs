namespace Interfaces
{
    public interface ICarriable : IInteractable
    {
        public void PickUp();
        public void Drop();
    }
}