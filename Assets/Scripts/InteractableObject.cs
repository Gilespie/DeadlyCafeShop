using UnityEngine;

public class InteractableObject : MonoBehaviour, IPickable, IHighlight
{
    bool _isPicked = false;
    public bool IsPicked => _isPicked;

    Rigidbody _rb;
    MeshRenderer _meshRenderer;

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

        _rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
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

    public void HighlightOn()
    {
        _meshRenderer.material.EnableKeyword("_EMISSION");
    }

    public void HighlightOff()
    {
        _meshRenderer.material.DisableKeyword("_EMISSION");
    }
}