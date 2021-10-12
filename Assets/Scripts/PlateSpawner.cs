using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : Generator
{
    private int _startingNumberOfPlates = 3;
    private List<Plate> _plates = new List<Plate>();
    private new void Start()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        HeldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>();
        canSetItem = false; // The player should not be able to place anything, only take from.
        
        for (int i = 1; i <= _startingNumberOfPlates; i++) SpawnPlates();
    }

    protected new void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing)
        {
            RemoveItem();
        }
    }

    protected override void RemoveItem()
    {
        if (_plates.Count <= 0) return;
        
        var plateToGive = _plates[_plates.Count - 1];
        if (HeldItem.SetHeldItem(plateToGive.gameObject))
        {
            Destroy(plateToGive.gameObject);
            _plates.RemoveAt(_plates.Count - 1);
        }
    }

    private void SpawnPlates()
    {
        Plate plate = Instantiate(generatedItem, new Vector3(transform.position.x, 2, transform.position.z),
            transform.rotation).GetComponent<Plate>();
        plate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        plate.GetComponent<Rigidbody>().useGravity = true;
        _plates.Add(plate);
    }
}