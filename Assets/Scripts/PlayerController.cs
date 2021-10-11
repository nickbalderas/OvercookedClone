using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private NavMeshAgent _agent;
    private readonly List<Collider> _interactableInRange = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HighlightInteractable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactableInRange.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            IInteractable utility = other.gameObject.GetComponent<IInteractable>();
            utility.Highlight(false);
            _interactableInRange.Remove(other);
        }
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

    private void HighlightInteractable()
    {
        if (_interactableInRange.Count > 0)
        {
            float closestSquareDistance = Mathf.Infinity;
            
            foreach (Collider collider in _interactableInRange)
            {
                Vector3 pos = collider.ClosestPointOnBounds(transform.position);
                Vector3 direction = pos - transform.position;
                float sqrDist = direction.sqrMagnitude;
                bool isPlayerFacing =  Vector3.Dot(transform.position, (collider.transform.position - transform.position).normalized) > 0;

                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (sqrDist < closestSquareDistance && isPlayerFacing)
                {
                    closestSquareDistance = sqrDist;
                    interactable.Highlight(true);
                }
                else interactable.Highlight(false);
            }
        }
    }
}