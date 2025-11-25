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

    public void InputArtificialUpdate()
    {
        _inputDirection.x = Input.GetAxisRaw("Horizontal");
        _inputDirection.z = Input.GetAxisRaw("Vertical");

        _mouseDelta.x = Input.GetAxis("Mouse X");
        _mouseDelta.y = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        _isTakedObject = Input.GetMouseButtonDown(0);
        _isObjectRotating = Input.GetMouseButton(1);
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
    }
}