using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundCollision : MonoBehaviour
{
    [SerializeField] AudioClip _clip;
    [SerializeField] float _intensity = 0.01f;
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        _audioSource.volume = collision.impulse.magnitude * _intensity;
        _audioSource.PlayOneShot(_clip);
    }
}