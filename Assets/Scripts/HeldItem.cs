using UnityEngine;

public class HeldItem : MonoBehaviour
{
    private GameObject _item;
    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && _item) DropItem();
    }

    // Sets the held item for the player
    public void SetHeldItem(GameObject item)
    {
        if (!item) return;
        
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        item.GetComponent<Rigidbody>().useGravity = false;
        _item = Instantiate(item, transform.position, transform.rotation, transform);
    }

    // Drops the held item in front of the player
    private void DropItem()
    {
        if (!_item) return;
        
        _item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _item.GetComponent<Rigidbody>().useGravity = true;
        Instantiate(_item, _player.transform.position + new Vector3(1, 1, 0), transform.rotation);
        Destroy(_item);
    }
}
