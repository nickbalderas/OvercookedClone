using UnityEngine;

namespace Interfaces
{
    public interface ICountertop : IInteractable
    {
        public GameObject placedItem { get; set; }
        public bool canSetItem { get; set; }
        public bool canGetItem { get; set; }
    }
}