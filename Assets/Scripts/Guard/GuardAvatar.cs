using UnityEngine;

public class GuardAvatar : MonoBehaviour
{
    Guard _parent;

    void Awake()
    {
        _parent = GetComponentInParent<Guard>();
    }

    public void StartPersuit()
    {
        _parent.StartPersuit();
    }

    public void StepSound()
    {
        _parent.StepSound();
    }
}