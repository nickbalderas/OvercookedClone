using System.Security.Cryptography;
using Interfaces;
using UnityEngine;

public class Countertop : MonoBehaviour, IInteractable
{
    protected Item _Item { get; set; }
    public bool IsPlayerFacing { get; set; }
    protected bool IsPlayerNear;

    protected virtual void Start()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        CleanCountertop();
    }
    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing)
        {
            if (_Item) RemoveItem();
            else PlaceItem();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IsPlayerNear = other.gameObject.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerNear = false;
    }
    
    protected virtual void PlaceItem()
    {
        if (_Item) return;

        var item = GameObject.Find("HeldItem").GetComponentInChildren<Item>();
        
        ICarriable carriable = item.GetComponent<ICarriable>();
        if (carriable == null) return;

        item.GetComponent<Rigidbody>().isKinematic = false;
        _Item = Instantiate(item, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
        Destroy(item.gameObject);
    }

    protected virtual void RemoveItem()
    {
        if (!_Item) return;

        if (!GameObject.Find("HeldItem").GetComponentInChildren<Item>())
        {
            var item = _Item;
            item.PickUp();
            _Item = null;
        }
        
    }
    
    protected virtual void CleanCountertop()
    {
        if (_Item) Destroy(_Item.gameObject);
    }

    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}