using UnityEngine;

public class Fantom : MonoBehaviour
{
    [SerializeField] Transform _centre;
    [SerializeField] AudioClip _clip;
    [SerializeField] float speed = 50f;
    AudioSource _audioSource;
    Vector3 _startPos;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _startPos = transform.position;
        PlaySound();
    }

    void Update()
    {
        float x = Mathf.PerlinNoise(Time.time * speed, 0f) * 6f - 3f;
        float z = Mathf.PerlinNoise(0f, Time.time * speed) * 6f - 3f;

        transform.position = _startPos + new Vector3(x, 0, z);
    }

    void PlaySound()
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
}