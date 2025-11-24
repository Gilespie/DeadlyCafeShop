using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float _walkspeed = 2f;
    [SerializeField] float _sprintSpeed = 5f;
    float _currentSpeed;
    Rigidbody _rb;
    Vector3 _moveDirection;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Movement(Vector3 dir, bool sprint)
    {
        _moveDirection = (transform.right * dir.x + transform.forward * dir.z);
        _currentSpeed = sprint ? _sprintSpeed : _walkspeed;
        _rb.MovePosition(_rb.position + _moveDirection.normalized * Time.fixedDeltaTime * _currentSpeed);
    }
}