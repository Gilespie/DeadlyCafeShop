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
        _movement = GetComponent<CharacterMovement>();
        _cameraFPS = GetComponentInChildren<CameraFPS>();
        _raycasting = GetComponentInChildren<Raycasting>();
        _controller = new CharacterInputController();
    }

    void Update()
    {
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
        _movement.Movement(_controller.InputDirection, _controller.IsSprinting);
    }
}