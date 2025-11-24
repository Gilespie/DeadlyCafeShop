using UnityEngine;

public class CharacterInputController
{
    Vector3 _inputDirection = new Vector3();
    public Vector3 InputDirection => _inputDirection;

    Vector2 _mouseDelta = Vector2.zero;
    public Vector2 MouseDelta => _mouseDelta;

    Character _character;
    bool isRotating = false;
    public bool IsRotating => isRotating;

    public CharacterInputController(Character character)
    {
        _character = character;
    }

    public void InputArtificialUpdate()
    {
        InputPC();
    }

    public void InputPC()
    {
        _inputDirection.x = Input.GetAxisRaw("Horizontal");
        _inputDirection.z = Input.GetAxisRaw("Vertical");

        _mouseDelta.x = Input.GetAxis("Mouse X");
        _mouseDelta.y = Input.GetAxis("Mouse Y");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _character.Sprint(_inputDirection);
        }

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

        if(Input.GetMouseButtonDown(0))
        {
            _character.Interact();
        }

        if (Input.GetMouseButtonDown(1))
            isRotating = true;

        if (Input.GetMouseButtonUp(1))
            isRotating = false;
    }

    public void FixedUpdateArtificial()
    { 
    }
}