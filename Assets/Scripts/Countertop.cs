using Interfaces;
using UnityEngine;

public class Countertop : MonoBehaviour, IInteractable
{
    protected Item CountertopItem { get; set; }
    public bool IsPlayerFacing { get; set; }
    protected bool IsPlayerNear { get; set; }
    protected Transform CountertopTransform;
    private const string Player = "Player";
    private const string EmissionColor = "_EmissionColor";
    private static readonly int EmissionColor1 = Shader.PropertyToID(EmissionColor);
    

    protected virtual void Awake()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        CountertopTransform = transform;
        CleanCountertop();
    }

    protected virtual void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !IsPlayerNear || !IsPlayerFacing) return;
        if (CountertopItem) RemoveItem();
        else PlaceItem();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IsPlayerNear = other.gameObject.CompareTag(Player);
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerNear = false;
    }
    
    protected virtual void PlaceItem()
    {
        if (CountertopItem) return;

        var heldItem = HeldItem.GetItem();

        if (!heldItem) return;
      
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        var position = CountertopTransform.position;
        CountertopItem = Instantiate(heldItem, new Vector3(position.x, 2, position.z), CountertopTransform.rotation);
        Destroy(heldItem.gameObject);
    }

    protected virtual void RemoveItem()
    {
        if (!CountertopItem) return;
        if(CountertopItem.PickUp()) CountertopItem = null;
    }
    
    protected virtual void CleanCountertop()
    {
        if (CountertopItem) Destroy(CountertopItem.gameObject);
    }

    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor(EmissionColor1, color);
    }
}