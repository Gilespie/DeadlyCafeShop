using UnityEngine;

public class CharacterInputController
{
    Vector3 _inputDirection = new Vector3();
    public Vector3 InputDirection => _inputDirection;

    Vector2 _mouseDelta = Vector2.zero;
    public Vector2 MouseDelta => _mouseDelta;

    bool _isObjectRotating = false;
    public bool IsObjectRotating => _isObjectRotating;

    bool _isSprinting = false;
    public bool IsSprinting => _isSprinting;

    bool _isTakedObject = false;
    public bool IsTakedObject => _isTakedObject;
    bool _isEscapePressed = false;
    public bool IsEscapePressed => _isEscapePressed;

    public void InputArtificialUpdate()
    {
        _inputDirection.x = Input.GetAxisRaw("Horizontal");
        _inputDirection.z = Input.GetAxisRaw("Vertical");

        _mouseDelta.x = Input.GetAxis("Mouse X");
        _mouseDelta.y = Input.GetAxis("Mouse Y");

        _isEscapePressed = Input.GetKeyDown(KeyCode.Escape);
        _isTakedObject = Input.GetMouseButtonDown(0);
        _isObjectRotating = Input.GetMouseButton(1);
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
    }
}