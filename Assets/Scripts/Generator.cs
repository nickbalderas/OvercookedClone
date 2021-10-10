using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject generatedItem;
    private GameObject _itemOnGenerator;
    private bool _isPlayerNear;
    private HeldItem _heldItem;
    private bool _isPlayerFacing;

    private void Start()
    {
        _heldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>();
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerNear)
        {
            if (_itemOnGenerator) PickupItem();
            else SpawnItem();
        }

        if (_isPlayerFacing)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.gray);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _isPlayerNear = other.gameObject.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        _isPlayerNear = false;
        _isPlayerFacing = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerFacing = Vector3.Dot(other.transform.forward, (transform.position - other.transform.position).normalized) > 0;
        }
    }

    private void SpawnItem()
    {
        _itemOnGenerator = Instantiate(generatedItem, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    }

    private void PickupItem()
    {
        _heldItem.SetHeldItem(_itemOnGenerator);
        Destroy(_itemOnGenerator);
    }
}
