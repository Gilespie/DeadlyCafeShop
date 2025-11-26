using UnityEngine;

public class Raycasting : MonoBehaviour
{
    [Header("Rays")]
    RaycastHit _interactHit;

    [Header("Pickup")]
    [SerializeField] Transform _itemHolder;
    IPickable _currentItem;
    public IPickable CurrentItem => _currentItem;

    [Header("Settings")]
    [SerializeField] float _interactRayDistance = 2f;
    [SerializeField] LayerMask _interactLayer;

    IHighlight _lastHighlight;

    void Update()
    {
        HighlightHover();
    }

    public void HighlightHover()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _interactHit, _interactRayDistance, _interactLayer))
        {
            if (_interactHit.collider.TryGetComponent(out IHighlight highlight))
            {
                if (_lastHighlight != highlight)
                {
                    _lastHighlight?.HighlightOff();
                    highlight.HighlightOn();
                    _lastHighlight = highlight;
                }
                return;
            }
        }

        if (_lastHighlight != null)
        {
            _lastHighlight.HighlightOff();
            _lastHighlight = null;
        }
    }

    public void Interact()
    {
        if (_currentItem != null)
        {
            if (Physics.Raycast(transform.position, transform.forward,
                                out _interactHit, _interactRayDistance, _interactLayer))
            {
                if (_interactHit.collider.TryGetComponent(out IInteractable interactable))
                {
                    bool used = interactable.Interact(this);

                    if (used) return;
                }
            }

            _currentItem.OnDrop();
            _currentItem = null;
            return;
        }

        bool interacted = false;

        if (Physics.Raycast(transform.position, transform.forward,
                            out _interactHit, _interactRayDistance, _interactLayer))
        {
            if (_interactHit.collider.TryGetComponent(out IInteractable interactable))
            {
                interacted = interactable.Interact(this);
            }

            if (!interacted && _interactHit.collider.TryGetComponent(out IPickable pickable))
            {
                if (pickable is Cup cup && cup.IsLocked)
                    return;

                if (_currentItem != null)
                {
                    _currentItem.OnDrop();
                    _currentItem = null;
                }

                pickable.OnPickUp(_itemHolder);
                _currentItem = pickable;
            }
        }
    }

    public void RotateHeldItem(Vector2 mouseInput)
    {
        if (_currentItem == null) return;

        if (_currentItem is InteractableObject obj)
        {
            obj.Rotate(mouseInput);
        }
    }

    public void PickItem(IPickable item)
    {
        if (_currentItem != null)
        {
            _currentItem.OnDrop();
            _currentItem = null;
        }

        item.OnPickUp(_itemHolder);
        _currentItem = item;
    }

    public void ClearItem() => _currentItem = null;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _interactRayDistance);
    }
}