using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _camera;
    private NavMeshAgent _agent;
    public List<Collider> interactableInRange = new List<Collider>();
    private const string Interactable = "Interactable";
    private Transform _transform;
    private float _closestSquareDistance;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _transform = transform;
    }

    void Update()
    {
        HandleMovement();
        if (interactableInRange.Count > 0) HighlightInteractable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Interactable)) interactableInRange.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(Interactable)) return;

        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        interactable.Highlight(false);
        interactableInRange.Remove(other);
    }

    private void HandleMovement()
    {
        if (_gameManager.gamePaused) return;
        if (!Input.GetMouseButton(0))
        {
            _playerAnimator.SetFloat("Speed_f", 0f);
            return;
        };

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            _agent.destination = hit.point;
            _playerAnimator.SetFloat("Speed_f", 20f);
        }
    }

    private void HighlightInteractable()
    {
        if (interactableInRange.Count <= 0) return;

        _closestSquareDistance = Mathf.Infinity;
        var playerPosition = _transform.position;

        foreach (var colliderInRange in interactableInRange)
        {
            var isPlayerFacing =
                Vector3.Dot(playerPosition, (colliderInRange.transform.position - playerPosition).normalized) > 0;
            var sqrDist = GetSquareDistanceFrom(colliderInRange);

            var interactable = colliderInRange.GetComponent<IInteractable>();
            if (IsPlayerClose(sqrDist) && isPlayerFacing)
            {
                _closestSquareDistance = GetSquareDistanceFrom(colliderInRange);
                interactable.IsPlayerFacing = true;
                interactable.Highlight(true);
            }
            else
            {
                interactable.IsPlayerFacing = false;
                interactable.Highlight(false);
            }
        }
    }

    private bool IsPlayerClose(float sqrDist)
    {
        return sqrDist < _closestSquareDistance;
    }

    private float GetSquareDistanceFrom(Collider colliderInRange)
    {
        var pos = colliderInRange.ClosestPointOnBounds(transform.position);
        var direction = pos - _transform.position;
        return direction.sqrMagnitude;
    }
}