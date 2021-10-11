using Interfaces;
using UnityEngine;

public class Ingredient : MonoBehaviour, IIngredient
{
    public bool isCut { get; set; }
    public bool canPickup { get; set; }
    public GameObject prefab;
    
    
    public void Highlight(bool indicator)
    {
    }

    public GameObject PickUp()
    {
        return gameObject;
    }

    public void Drop()
    {
    }
}