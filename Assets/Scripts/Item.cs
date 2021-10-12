using Interfaces;
using UnityEngine;

public class Item : MonoBehaviour, ICarriable
{
    public bool canPickup { get; set; }
    public bool IsPlayerFacing { get; set; }
    public GameObject prefab { get; set; }
    protected bool IsPlayerNear;
    protected PlayerController _player;
    private Rigidbody rb;
    private BoxCollider coll;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (prefab) prefab.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }
    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IsPlayerNear = other.gameObject.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerNear = false;
    }

    public virtual void PickUp()
    {
        var heldItem = GameObject.Find("HeldItem");
        if (heldItem.GetComponent<Item>()) return;
        
        transform.position = heldItem.transform.position;
        transform.rotation = heldItem.transform.rotation;
        rb.isKinematic = true;
        transform.parent = heldItem.transform;
        Highlight(false);
    }

    public void Drop()
    {
        var heldItem = GameObject.Find("HeldItem").GetComponentInChildren<Item>();
        if (!heldItem) return;

        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.transform.parent = null;
    }
    
    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}