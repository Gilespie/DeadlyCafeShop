using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Character : MonoBehaviour
{
    CameraFPS _cameraFPS;
    CharacterInputController _controller;
    CharacterMovement _movement;
    Raycasting _raycasting;

    void Awake()
    {
        _controller = new CharacterInputController(this);
        _movement = GetComponent<CharacterMovement>();
        _cameraFPS = GetComponentInChildren<CameraFPS>();
        _raycasting = GetComponentInChildren<Raycasting>();
    }

    void Update()
    {
        _controller.InputArtificialUpdate();

        if (!_controller.IsRotating)
        {
            _cameraFPS.RotateCamera(_controller.MouseDelta);
            float yaw = _controller.MouseDelta.x * _cameraFPS.LookSensitivity;
            transform.Rotate(Vector3.up, yaw, Space.Self);
        }

        RotateHeldItem(_controller.MouseDelta);

        _raycasting.HighlightHover();
    }

    void FixedUpdate()
    {
        if(_controller.InputDirection.sqrMagnitude > 0.001f)
        {
            _movement.Movement(_controller.InputDirection);
        }
    }

    public void Interact()
    {
        _raycasting.Interact();
    }

    public void Sprint(Vector3 dir)
    {
        _movement.Sprint(dir);
    }

    public void RotateHeldItem(Vector2 md)
    {
        _raycasting.RotateHeldItem(md.x, md.y);
    }
}