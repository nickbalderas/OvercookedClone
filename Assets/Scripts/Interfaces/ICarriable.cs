using UnityEngine;

namespace Interfaces
{
    public interface ICarriable : IInteractable
    {
        public void PickUp();
        public void Drop();
        public GameObject Prefab { get; set; }
    }
}