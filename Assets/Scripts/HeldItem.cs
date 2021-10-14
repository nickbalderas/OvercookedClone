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

    public static bool IsHeldItemPlate()
    {
        return GameObject.Find("HeldItem").GetComponentInChildren<Plate>();
    }
}