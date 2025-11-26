using System.Collections;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    [SerializeField] float _thunderInterval = 7f;
    int _randomIndex;

    AudioSource _audioSource;
    Animator _animator;

    void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        StartCoroutine(ThunderRoutine());
    }

    IEnumerator ThunderRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_thunderInterval);
            RandomClip();
            _animator.Play("Thundering", 0, 0f);
            yield return null;
        }
    }

    public void RandomClip()
    {
        _randomIndex = Random.Range(0, _clips.Length);
        _audioSource.clip = _clips[_randomIndex];
        _audioSource.Play();
    }
}