using UnityEngine;

public class Generator : Countertop
{
    public Item generatedItem;
    
    protected new void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing)
        {
            if (_Item) RemoveItem();
            else if (GameObject.Find("HeldItem").GetComponentInChildren<Item>()) PlaceItem();
            else SpawnItem();
        }
    }
    
    private void SpawnItem()
    {
        _Item = Instantiate(generatedItem, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    }
}