using Interfaces;
using UnityEngine;

public class Countertop : MonoBehaviour, IInteractable
{
    public GameObject placedItem { get; set; }
    public bool canSetItem { get; set; }
    public bool canGetItem { get; set; }
    public bool IsPlayerFacing { get; set; }
    protected bool IsPlayerNear;
    protected HeldItem HeldItem;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        HeldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>();
        CleanCountertop();
    }
    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNear && IsPlayerFacing)
        {
            if (placedItem) RemoveItem();
            else PlaceItem(HeldItem.TransferItem());
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
    
    protected virtual void PlaceItem(GameObject item)
    {
        if (!canSetItem) return;

        ICarriable carriable = item.GetComponent<ICarriable>();
        if (carriable == null) return;

        canSetItem = false;
        canGetItem = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item.GetComponent<Rigidbody>().useGravity = true;
        placedItem = Instantiate(item, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    }

    protected void RemoveItem()
    {
        if (!canGetItem) return;

        if (HeldItem.SetHeldItem(placedItem)) CleanCountertop();
    }
    
    protected virtual void CleanCountertop()
    {
        if (placedItem) Destroy(placedItem);
        canSetItem = true;
        canGetItem = false;
    }

    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}