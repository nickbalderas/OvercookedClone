using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject generatedItem;
    private GameObject _itemOnGenerator;
    private bool _isPlayerNear;
    private HeldItem _heldItem;

    private void Start()
    {
        _heldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerNear)
        {
            if (_itemOnGenerator) PickupItem();
            else SpawnItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isPlayerNear = other.gameObject.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        _isPlayerNear = false;
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
