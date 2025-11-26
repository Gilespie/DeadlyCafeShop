using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IObservable, IInteractable
{
    [SerializeField] private Transform handPoint;
    GuardAnimationController _animations;
    GuardMovement _movement;
    Animator _animator;
    bool _isWaiting = false;
    bool _isFollowingPlayer = false;
    GameObject _receivedCup;
    private bool _hasCup = false;
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
            var target = _movement?.Target;
            if (target != null && target.IsDead)
            {
                _isFollowingPlayer = false;
                _movement.Stop();
                _animations.UpdateSpeed(0f);
                return;
            }

            _movement.Persuit();
            _animations.UpdateSpeed(_movement.CurrentSpeed);
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (!_isFollowingPlayer) return;
        
        if (collision.gameObject.TryGetComponent(out IDamageable character))
        {
            character.InstanceKill();
        }
    }

    public bool Interact(Raycasting interactor)
    {
        if (_hasCup) return false;

        if (interactor.CurrentItem is Cup cup && cup.IsReady)
        {
            TakeCup(cup, interactor);
            interactor.ClearItem();
            return true;
        }

        return false;
    }

    private void TakeCup(Cup cup, Raycasting interactor)
    {
        _hasCup = true;

        _receivedCup = cup.gameObject;

        Rigidbody rb = _receivedCup.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        Collider col = _receivedCup.GetComponent<Collider>();
        if (col) col.enabled = false;

        _receivedCup.transform.SetParent(handPoint);
        _receivedCup.transform.localPosition = Vector3.zero;
        _receivedCup.transform.localRotation = Quaternion.identity;

        _animations.CallTrigger();
    }

    public void ThrowCup()
    {
        if (_receivedCup == null)
        {
            var cupComp = handPoint.GetComponentInChildren<Cup>();
            if (cupComp != null)
            {
                _receivedCup = cupComp.gameObject;
            }
            else
            {
                _hasCup = false;
                return;
            }
        }

        _receivedCup.transform.SetParent(null);

        Rigidbody rb = _receivedCup.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        foreach (var col in _receivedCup.GetComponents<Collider>())
        {
            col.enabled = true;
        }

        if (rb != null)
            rb.AddForce(transform.forward * 4f + Vector3.up * 2f, ForceMode.Impulse);

        _hasCup = false;
        _receivedCup = null;
    }

    public void StartPersuit()
    {
        _isFollowingPlayer = true;
        _isWaiting = false;
        EventManager.Trigger(EventType.GuardStartPersuit);
    }

    public void StepSound()
    {
        NotifyObservers();
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