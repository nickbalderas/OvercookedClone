using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : Generator
{
    
    private const int StartingNumberOfPlates = 3;
    private readonly List<Plate> _plates = new List<Plate>();
    
    private void Start()
    {
        for (var i = 1; i <= StartingNumberOfPlates; i++) SpawnPlate();
    }

    protected new void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing) RemoveItem();
    }

    protected override void RemoveItem()
    {
        if (_plates.Count <= 0 || HeldItem.GetItem()) return;
        
        var plateToGive = _plates[_plates.Count - 1];
        plateToGive.PickUp();
        _plates.RemoveAt(_plates.Count - 1);
    }

    public void SpawnPlate()
    {
        var position = CountertopTransform.position;
        var plate = Instantiate(generatedItem as Plate, new Vector3(position.x, 2, position.z),
            CountertopTransform.rotation);
        var plateRb = plate.GetComponent<Rigidbody>();
        plateRb.constraints = RigidbodyConstraints.FreezeRotation;
        plateRb.useGravity = true;
        _plates.Add(plate);
    }
}