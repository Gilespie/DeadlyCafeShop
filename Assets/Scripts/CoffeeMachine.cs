using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour, IInteractable, IObservable
{
    [SerializeField] Transform _cupPosition;
    [SerializeField] float _prepareTime = 3f;
    Cup _currentCup;
    bool _isPreparing;
    CoffeeButton _currentButton;
    List<IObserver> _observers = new List<IObserver>();

    public bool CanActivate => !_isPreparing && _currentCup != null;
    public bool CanTakeCup => _currentCup != null && !_isPreparing && _currentCup.HasCoffee;

    void Awake()
    {
        _currentButton = GetComponentInChildren<CoffeeButton>();
    }
    public bool Interact(Raycasting interactor)
    {
        if (_currentCup == null && interactor.CurrentItem is Cup cup)
        {
            PlaceCup(cup, interactor);
            return true;
        }

        if (CanTakeCup)
        {
            Cup c = TakeCup();
            interactor.PickItem(c);
            return true;
        }

        if (CanActivate)
        {
            ActivateMachine();
            return true;
        }

        return false;
    }

    void PlaceCup(Cup cup, Raycasting interactor)
    {
        cup.OnPlace(_cupPosition);

        interactor.ClearItem();
        _currentCup = cup;
    }

    public void ActivateMachine() => StartCoroutine(PrepareRoutine());

    IEnumerator PrepareRoutine()
    {
        NotifyObservers();
        EventManager.Trigger(EventType.OnCoffeeMachineUsed);
        _currentCup.LockCup(true);
        _isPreparing = true;
        yield return new WaitForSeconds(_prepareTime);
        _currentCup.FillCoffee();
        _currentCup.LockCup(false);
        _currentButton.ResetButton();
        _isPreparing = false;
    }

    Cup TakeCup()
    {
        Cup c = _currentCup;
        _currentCup = null;
        c.transform.SetParent(null);
        return c;
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