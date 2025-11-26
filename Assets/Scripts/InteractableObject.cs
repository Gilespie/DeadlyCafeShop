using UnityEngine;

public class InteractableObject : MonoBehaviour, IPickable, IHighlight
{
    protected bool _isPicked = false;
    public bool IsPicked => _isPicked;

    protected Rigidbody _rb;
    protected MeshRenderer _meshRenderer;

    [Header("Pickup Settings")]
    [SerializeField] protected float _throwForce = 2f;
    [SerializeField] protected float _speedRotation = 200f;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public virtual void OnDrop()
    {
        _isPicked = false;

        transform.SetParent(null);
        _rb.isKinematic = false;
        _rb.detectCollisions = true;

        _rb.AddForce(Camera.main.transform.forward * _throwForce, ForceMode.Impulse);
    }

    public virtual void OnPickUp(Transform holder)
    {
        _isPicked = true;

        _rb.isKinematic = true;
        _rb.detectCollisions = false;

        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void OnPlace(Transform parent)
    {
        _isPicked = false;

        transform.SetParent(parent, worldPositionStays: false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (_rb != null)
        {
            _rb.isKinematic = true;
            _rb.detectCollisions = true;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }

    public void HighlightOn()
    {
        _meshRenderer.material.EnableKeyword("_EMISSION");
    }

    public void HighlightOff()
    {
        _meshRenderer.material.DisableKeyword("_EMISSION");
    }

    public virtual void Rotate(Vector2 mouseInput)
    {
        if (!_isPicked) return;

        transform.Rotate(Camera.main.transform.up, mouseInput.x * _speedRotation * Time.deltaTime, Space.World);
        transform.Rotate(Camera.main.transform.right, -mouseInput.y * _speedRotation * Time.deltaTime, Space.World);
    }
}