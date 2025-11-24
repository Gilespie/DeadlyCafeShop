using UnityEngine;

public interface IPickable
{
    void OnPickUp(Transform holder);
    void OnDrop();
    bool IsPicked { get; }
}