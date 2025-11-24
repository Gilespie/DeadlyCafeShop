using UnityEngine;

public class CameraFPS : MonoBehaviour
{
    [SerializeField] float _lookSensitivity = 2f;
    public float LookSensitivity => _lookSensitivity;
    [SerializeField] bool _invertY = false;
    [SerializeField] float _maxPitch = 80f;
    float _pitch = 0f;
    Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCamera(Vector2 inputDelta)
    {
        Vector2 md = inputDelta * _lookSensitivity;

        float invert = _invertY ? 1f : -1f;
        _pitch += md.y * invert;
        _pitch = Mathf.Clamp(_pitch, -_maxPitch, _maxPitch);
        _camera.transform.localEulerAngles = new Vector3(_pitch, 0f, 0f);
    }
}