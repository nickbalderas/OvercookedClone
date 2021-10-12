using Interfaces;
using UnityEngine;

public class HeldItem : MonoBehaviour, IInteractable
{
    public GameObject heldItem { get; set; }
    public bool IsPlayerFacing { get; set; }
    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && heldItem) DropItem();
    }

    // Sets the held item for the player
    public bool SetHeldItem(GameObject item)
    {
        if (heldItem || !item) return false;
        
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        item.GetComponent<Rigidbody>().useGravity = false;
        heldItem = Instantiate(item, transform.position, transform.rotation, transform);
        return true;
    }

    public GameObject TransferItem()
    {
        GameObject item = heldItem;
        Destroy(heldItem);
        return item;
    }

    // Drops the held item in front of the player
    private void DropItem()
    {
        if (!heldItem) return;
        
        heldItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        heldItem.GetComponent<Rigidbody>().useGravity = true;
        Instantiate(heldItem, _player.transform.position + new Vector3(1, 1, 0), transform.rotation);
        Destroy(heldItem);
    }

    
    public void Highlight(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}
