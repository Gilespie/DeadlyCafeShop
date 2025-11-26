using UnityEngine;

public class ScreamerSFX : MonoBehaviour
{
    [SerializeField] float _speedMove = 1f;
    AudioSource _audioSource;
    bool _isTriggered = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EventType.OnCoffeeMachineUsed, OnTriggeredEvent);
    }

    private void Update()
    {
        if (_isTriggered)
        {
            transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);
        }
    }


    void OnDisable()
    {
        EventManager.Unsubscribe(EventType.OnCoffeeMachineUsed, OnTriggeredEvent);
    }

    void OnTriggeredEvent(params object[] parameters)
    {
        _isTriggered = true;
        _audioSource.Play();
    }
}