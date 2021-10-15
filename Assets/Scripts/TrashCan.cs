using UnityEngine;

public class TrashCan : Countertop
{
    protected override void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !IsPlayerNear || !IsPlayerFacing) return;
        PlaceItem();
    }

    protected override void PlaceItem()
    {
        var isPlate = HeldItem.IsHeldItemPlate();
        var heldItem = HeldItem.GetItem();
        if (!heldItem) return;
        
        if (heldItem && !isPlate)Destroy(heldItem.gameObject);
        else heldItem.GetComponent<Plate>().ingredients.Clear();
    }
}