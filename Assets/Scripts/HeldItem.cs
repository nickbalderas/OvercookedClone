using UnityEngine;

public static class HeldItem
{
    // TODO: You might want to rethink this esp if they are getting called frequently.
    
    public static Item GetItem()
    {
        return GameObject.Find("HeldItem").GetComponentInChildren<Item>();
    }

    public static Transform GetHeldItemTransform()
    {
        return GameObject.Find("HeldItem").transform;
    }

    public static Plate HeldPlate()
    {
        return GameObject.Find("HeldItem").GetComponentInChildren<Plate>();
    }
    
    public static Ingredient HeldIngredient()
    {
        return GameObject.Find("HeldItem").GetComponentInChildren<Ingredient>();
    }
}