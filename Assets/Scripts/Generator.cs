using UnityEngine;

public class Generator : Countertop
{
    public Item generatedItem;
    
    protected new void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !IsPlayerNear || !IsPlayerFacing) return;
        if (CountertopItem) RemoveItem();
        else if (HeldItem.GetItem()) PlaceItem();
        else SpawnItem();
    }
    
    private void SpawnItem()
    {
        var position = CountertopTransform.position;
        CountertopItem = Instantiate(generatedItem, new Vector3(position.x, 2, position.z), CountertopTransform.rotation);
    }
}