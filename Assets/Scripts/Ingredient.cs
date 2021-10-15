using System;
using UnityEngine;

[Serializable]
public class Ingredient : Item
{
    public string ingredient;
    public bool isCut;

    public Model.Ingredient ConvertToModel()
    {
        return new Model.Ingredient(ingredient, isCut);
    }

    public override bool PickUp()
    {
        var heldPlate = HeldItem.HeldPlate();
        if (!heldPlate) return base.PickUp();
        //TODO: Better way of implementing? Need to remove collider from interactable in range before Destroy.
        var player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.interactableInRange.Remove(GetComponent<Collider>());
        
        heldPlate.AddIngredient(this);
        return false;
    }
}