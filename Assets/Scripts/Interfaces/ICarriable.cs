using UnityEngine;

namespace Interfaces
{
    public interface ICarriable : IInteractable
    {
        public bool canPickup { get; set; }
        public GameObject PickUp();
        public void Drop();
    }
}