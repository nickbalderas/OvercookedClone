using System.Collections.Generic;
using UnityEngine;

public class Plate : Item
{
    public List<Model.Ingredient> ingredients = new List<Model.Ingredient>();

    public override void PickUp()
    {
        var heldItem = GameObject.Find("HeldItem");
        var heldIngredient = heldItem.GetComponentInChildren<Ingredient>();
        if (heldItem && heldIngredient)
            AddIngredient(heldItem.GetComponentInChildren<Ingredient>());
        else base.PickUp();
    }

    private void AddIngredient(Ingredient ingredient)
    {
        //TODO: Should this condition act as Guard Clause
        if (!ingredient.isCut) return;

        ingredients.Add(ingredient.ConvertToModel());
        Destroy(ingredient.gameObject);
    }

    // public void RemoveIngredient(Ingredient ingredient)
    // {
    //     ingredientsOnPlate.Remove(ingredient);
    // }
    //
}