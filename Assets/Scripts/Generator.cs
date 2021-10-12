using UnityEngine;

public class Generator : Countertop
{
    public GameObject generatedItem;
    
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing)
        {
            if (placedItem) RemoveItem();
            else if (HeldItem.heldItem) PlaceItem(HeldItem.TransferItem());
            else SpawnItem();
        }
    }
    
    private void SpawnItem()
    {
        placedItem = Instantiate(generatedItem, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
        canGetItem = true;
        canSetItem = false;
    }
}