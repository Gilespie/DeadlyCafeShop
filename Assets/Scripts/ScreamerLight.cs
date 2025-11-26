using UnityEngine;

public class ScreamerLight : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.enabled = false;
    }

    void OnEnable()
    {
        EventManager.Subscribe(EventType.OnCoffeeMachineUsed, OnTriggeredEvent);

    }

    void OnDisable()
    {
        EventManager.Unsubscribe(EventType.OnCoffeeMachineUsed, OnTriggeredEvent);
    }

    void OnTriggeredEvent(params object[] parameters)
    {
        _animator.enabled = true;
    }
}