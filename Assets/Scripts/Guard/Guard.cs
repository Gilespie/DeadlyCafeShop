using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IObservable
{
    GuardAnimationController _animations;
    GuardMovement _movement;
    Animator _animator;
    bool _isWaiting = false;
    bool _isFollowingPlayer = false;

    List<IObserver> _observers = new List<IObserver>();

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animations = new GuardAnimationController(_animator);
        _movement = GetComponent<GuardMovement>();
    }

    void Start()
    {
        _movement.MoveToTarget();
        _animations.UpdateSpeed(_movement.CurrentSpeed);
    }

    void Update()
    {
        if (!_isFollowingPlayer && !_isWaiting)
        {
            if (_movement.ReachedTarget())
            {
                _isWaiting = true;
                _movement.Stop();
                _animations.UpdateSpeed(0f);
            }
        }

        if (_isFollowingPlayer)
        {
            _movement.Persuit();
            _animations.UpdateSpeed(_movement.CurrentSpeed);
        }
    }

    public void ReceiveCoffee()
    {
        if (!_isWaiting) return;

        Debug.Log("Guard: received coffee!");
        _animations.CallTrigger();
    }

    public void StartPersuit()
    {
        _isFollowingPlayer = true;
        _isWaiting = false;
    }

    public void StepSound()
    {
        NotifyObservers();
        Debug.Log("step sound");
    }

    #region OBSERVABLE IMPLEMENTATION
    public void Subscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void NotifyObservers(params object[] values)
    {
        foreach (var observer in _observers)
        {
            observer.UpdateData(values);
        }
    }
    #endregion
}