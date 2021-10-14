using UnityEngine;

public static class HeldItem
{
    public static Item GetItem()
    {
        return GameObject.Find("HeldItem").GetComponentInChildren<Item>();
    }

    public static Transform GetHeldItemTransform()
    {
        return GameObject.Find("HeldItem").transform;
    }
}