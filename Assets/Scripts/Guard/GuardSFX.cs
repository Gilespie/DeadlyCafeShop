using UnityEngine;

public class GuardSFX : MonoBehaviour, IObserver
{
    [SerializeField] AudioClip _Clip;
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        IObservable obs = GetComponentInParent<IObservable>();

        if (obs == null)
        {
            gameObject.SetActive(false);
            Debug.LogWarning("GuardSFX: No IObservable found in parent. Disabling GuardSFX.");
            return;
        }

        obs.Subscribe(this);
    }

    public void UpdateData(params object[] values)
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_Clip);
        Debug.Log("GuardSFX: Playing step sound.");
    }
}