using System.Collections.Generic;

public class Plate : Item
{
    public List<Model.Ingredient> ingredients = new List<Model.Ingredient>();
    //TODO: Does this change with difficulty? Reference Overcooked.
    public int maxIngredientsOnPlate = 1;

    public override bool PickUp()
    {
        var heldIngredient = HeldItem.HeldIngredient();
        if (!heldIngredient) return base.PickUp();
        AddIngredient(heldIngredient);
        return false;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        //TODO: Should isCut condition act as Guard Clause? Not scalable with multiple conditions.
        var ingredientToAdd = 1;
        if (!ingredient.isCut || ingredients.Count + ingredientToAdd > maxIngredientsOnPlate) return;

        ingredients.Add(ingredient.ConvertToModel());
        Destroy(ingredient.gameObject);
    }
}