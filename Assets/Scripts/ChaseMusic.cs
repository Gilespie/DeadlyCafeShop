using UnityEngine;

public class ChaseMusic : MonoBehaviour
{
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EventType.GuardStartPersuit, PlayMusic);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventType.GuardStartPersuit, PlayMusic);
    }

    void PlayMusic(params object[] parameters)
    {
        _audioSource.time = 72f;
        _audioSource.Play();
    }
}