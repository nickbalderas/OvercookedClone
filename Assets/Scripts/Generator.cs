using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject generatedItem;
    private GameObject _itemOnGenerator;
    private bool _isPlayerNear;
    private PlayerController _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
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
        _isPlayerNear = !other.gameObject.CompareTag("Player");
    }

    private void SpawnItem()
    {
        _itemOnGenerator = Instantiate(generatedItem, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation);
    }

    private void PickupItem()
    {
        _player.SetHeldItem(_itemOnGenerator);
        Destroy(_itemOnGenerator);
    }
}
