using UnityEngine;

public class CameraFPS : MonoBehaviour
{
    [SerializeField] float _lookSensitivity = 2f;
    public float LookSensitivity => _lookSensitivity;
    [SerializeField] bool _invertY = false;
    [SerializeField] float _xRotationLimit = 80f;
    float _xRotation = 0f;
    float _yRotation = 0f;
    public float YRot => _yRotation;
    Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCamera(Vector2 inputDelta)
    {
        Vector2 md = inputDelta * _lookSensitivity;

        _yRotation = inputDelta.x * _lookSensitivity;

        float invert = _invertY ? 1f : -1f;

        _xRotation += md.y * invert;
        _xRotation = Mathf.Clamp(_xRotation, -_xRotationLimit, _xRotationLimit);

        _camera.transform.localEulerAngles = new Vector3(_xRotation, 0f, 0f);
    }
}