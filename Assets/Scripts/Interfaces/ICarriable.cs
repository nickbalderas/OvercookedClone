using UnityEngine;

namespace Interfaces
{
    public interface ICarriable : IInteractable
    {
        public bool canPickup { get; set; }
        public void PickUp();
        public void Drop();
        public GameObject prefab { get; set; }
    }
}