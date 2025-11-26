using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallableObject : MonoBehaviour
{
    [SerializeField] float _fallDelay = 10f;
    [SerializeField] float _force = 10f;
    Rigidbody _rb;
    float _timer;
    bool _hasFallen = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }
    
    void Update()
    {
        if(_hasFallen) return;

        _timer += Time.deltaTime;

        if(_timer >= _fallDelay)
        {
            _rb.isKinematic = false;
            _rb.AddForce(Vector3.forward * _force, ForceMode.Impulse);
            _hasFallen = true;
        }
    }
}