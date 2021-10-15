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
        var heldItem = HeldItem.GetItem();
        if (!heldItem) return;
        
        if (heldItem && !HeldItem.HeldPlate())Destroy(heldItem.gameObject);
        else heldItem.GetComponent<Plate>().ingredients.Clear();
    }
}