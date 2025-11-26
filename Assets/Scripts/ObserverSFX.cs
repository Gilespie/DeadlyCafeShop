using UnityEngine;

public class ObserverSFX : MonoBehaviour, IObserver
{
    [SerializeField] protected AudioClip _Clip;
    protected AudioSource _audioSource;

    protected void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        IObservable obs = GetComponentInParent<IObservable>();

        if (obs == null)
        {
            gameObject.SetActive(false);
            Debug.LogWarning(gameObject.name + " No IObservable found in parent. Disabling ObserverSFX");
            return;
        }

        obs.Subscribe(this);
    }

    public virtual void UpdateData(params object[] values)
    {
        _audioSource.PlayOneShot(_Clip);
    }
}