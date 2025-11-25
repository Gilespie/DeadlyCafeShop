using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Character : MonoBehaviour, IDamageable
{
    CameraFPS _cameraFPS;
    CharacterInputController _controller;
    CharacterMovement _movement;
    Raycasting _raycasting;
    Rigidbody _rb;
    bool _isDead = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _movement = GetComponent<CharacterMovement>();
        _cameraFPS = GetComponentInChildren<CameraFPS>();
        _raycasting = GetComponentInChildren<Raycasting>();
        _controller = new CharacterInputController();
    }

    void Update()
    {
        if(_isDead) return;

        _controller.InputArtificialUpdate();
        _raycasting.HighlightHover();

        if(_controller.IsTakedObject)
        {
            _raycasting.Interact();
        }

        if (_controller.IsObjectRotating)
        { 
            _raycasting.RotateHeldItem(_controller.MouseDelta);
        }
        else
        {
            _cameraFPS.RotateCamera(_controller.MouseDelta);
            transform.Rotate(Vector3.up, _cameraFPS.YRot, Space.Self);
        }
    }

    void FixedUpdate()
    {
        if(_isDead) return;

        _movement.Movement(_controller.InputDirection, _controller.IsSprinting);
    }

    public void InstanceKill()
    {
        _rb.freezeRotation = false;
        _isDead = true;
    }
}