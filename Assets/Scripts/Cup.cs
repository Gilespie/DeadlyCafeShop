using UnityEngine;

public class Cup : InteractableObject, IInteractable
{
    [SerializeField] Transform _lidPosition;
    public Transform LidPosition => _lidPosition;

    bool _hasCoffee = false;
    public bool HasCoffee => _hasCoffee;

    bool _hasLid = false;
    public bool HasLid => _hasLid;

    public void FillCoffee() => _hasCoffee = true;
    public void PutLid() => _hasLid = true;
    public bool IsReady => HasCoffee && HasLid;
    bool _isLocked = false;
    public bool IsLocked => _isLocked;

    public bool Interact(Raycasting interactor)
    {
        if (!HasCoffee)
            return false;

        if (interactor.CurrentItem is Lid lid)
        {
            lid.AttachToCup(this);
            interactor.ClearItem();
            return true;
        }

        return false;
    }

    public void LockCup(bool value) => _isLocked = value;
}