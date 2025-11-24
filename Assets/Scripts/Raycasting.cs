using UnityEngine;

public class Raycasting : MonoBehaviour
{
    [Header("Rays")]
    Ray _interactRay;
    RaycastHit _interactHit;

    [Header("Pickup")]
    [SerializeField] Transform _itemHolder;
    IPickable _currentItem;

    [Header("Settings")]
    [SerializeField] float _interactRayDistance = 2f;
    [SerializeField] LayerMask _interactLayer;
    IHighlight _lastHighlight;

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
            _currentItem.OnDrop();
            _currentItem = null;
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out _interactHit, _interactRayDistance, _interactLayer))
        {
            if (_interactHit.collider.TryGetComponent(out IPickable pickable))
            {
                pickable.OnPickUp(_itemHolder);
                _currentItem = pickable;
            }
        }
    }

    public void RotateHeldItem(Vector2 mouseInput)
    {
        if (_currentItem == null) return;

        Transform t = (_currentItem as MonoBehaviour).transform;

        float speed = 200f;

        t.Rotate(Camera.main.transform.up, mouseInput.x * speed * Time.deltaTime, Space.World);
        t.Rotate(Camera.main.transform.right, -mouseInput.y * speed * Time.deltaTime, Space.World);
    }

    void OnDrawGizmos()
     {
        _interactRay = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_interactRay.origin, _interactRay.origin + _interactRay.direction * _interactRayDistance);
     }
}