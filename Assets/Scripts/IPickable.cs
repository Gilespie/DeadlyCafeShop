using UnityEngine;

public interface IPickable
{
    void OnPickUp(Transform holder);
    void OnDrop();
    void OnPlace(Transform parent);
    bool IsPicked { get; }
}