using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private HeldItem _heldItem;
    private Camera _camera;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        if (_camera == null)
        {
            Debug.LogError("Here");
        }

        _agent = GetComponent<NavMeshAgent>();
        _heldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleItemDrop();
    }

    public void SetHeldItem(GameObject item)
    {
        if (!item) return;
        _heldItem.SetHeldItem(item);
    }

    private void HandleMovement()
    {
        if (!Input.GetMouseButton(0)) return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            _agent.destination = hit.point;
        }
    }

    private void HandleItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.E) && _heldItem.GetHeldItem())
        {
            _heldItem.DropItem();
        }
    }
}