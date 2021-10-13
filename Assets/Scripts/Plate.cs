using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : Item
{
    public List<Ingredient> _ingredientsOnPlate = new List<Ingredient>();

    public override void PickUp()
    {
        var heldItem = GameObject.Find("HeldItem");
        var heldIngredient = heldItem.GetComponentInChildren<Ingredient>();
        if (heldItem && heldIngredient)
            AddIngredient(heldItem.GetComponentInChildren<Ingredient>());
        else base.PickUp();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (!ingredient.isCut) return;
        
        _ingredientsOnPlate.Add(ingredient);
        ingredient.GetComponent<Rigidbody>().isKinematic = false;
        Instantiate(ingredient, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
        ingredient.transform.parent = transform;
        Destroy(ingredient.gameObject);
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