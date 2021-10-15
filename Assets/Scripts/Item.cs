using Interfaces;
using UnityEngine;

public class Item : MonoBehaviour, ICarriable
{
    public bool IsPlayerFacing { get; set; }
    private bool _isPlayerNear;
    private Rigidbody _rb;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    protected Transform ItemTransform;
    private const string Player = "Player";
    private const string Emission = "_EMISSION";
    private bool _isPlayerHolding;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        ItemTransform = transform;
        gameObject.GetComponent<Renderer>().material.EnableKeyword(Emission);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerNear && IsPlayerFacing) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isPlayerNear = other.gameObject.CompareTag(Player);
    }

    private void OnTriggerExit(Collider other)
    {
        _isPlayerNear = false;
    }

    public virtual bool PickUp()
    {
        var heldItem = HeldItem.GetItem();
        if (heldItem) return false;
        
        var heldItemTransform = HeldItem.GetHeldItemTransform();
        ItemTransform.SetPositionAndRotation(heldItemTransform.position, heldItemTransform.rotation);
        ItemTransform.SetParent(heldItemTransform);
        _rb.isKinematic = true;
        Highlight(false);
        _isPlayerHolding = true;
        return true;
    }

    public void Drop()
    {
        if (!_isPlayerHolding) return;
        
        ItemTransform.parent = null;
        _rb.isKinematic = false;
        _isPlayerHolding = false;
    }
    
    public virtual void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor(EmissionColor, color);
    }
}