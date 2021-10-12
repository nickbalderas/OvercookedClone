using System.Collections.Generic;
using System.Linq;

public class Plate : Item
{
    private List<Ingredient> _ingredientsOnPlate = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredientsOnPlate.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        _ingredientsOnPlate.Remove(ingredient);
    }

    public bool DoesContainIngredients(List<Ingredient> ingredients)
    {
        return _ingredientsOnPlate.SequenceEqual(ingredients);
    }
}