using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float _walkspeed = 2f;
    [SerializeField] float _sprintSpeed = 5f;
    Rigidbody _rb;
    Vector3 _moveDirection;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Movement(Vector3 dir)
    {
        _moveDirection = (transform.right * dir.x + transform.forward * dir.z);
        _rb.MovePosition(_rb.position + _moveDirection.normalized * Time.fixedDeltaTime * _walkspeed);
    }

    public void Sprint(Vector3 dir)
    {
        _moveDirection = (transform.right * dir.x + transform.forward * dir.z);
        _rb.MovePosition(_rb.position + _moveDirection.normalized * Time.fixedDeltaTime * _sprintSpeed);
    }
}