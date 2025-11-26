using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] PauseScreen _pauseScreen;
    [SerializeField] LooseScreen _loseScreen;
    [SerializeField] float _delayToActivatePause = 2f;
    [SerializeField] float _forceImpulse = 2f;
    CameraFPS _cameraFPS;
    CharacterInputController _controller;
    CharacterMovement _movement;
    Raycasting _raycasting;
    Rigidbody _rb;
    bool _isDead = false;
    public bool IsDead => _isDead;

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

        if (_controller.IsEscapePressed)
        {
            if (!ScreenManager.Instance.IfScreenActive(_pauseScreen))
            {
                ScreenManager.Instance.ActivateScreen(_pauseScreen);
                CursorState(true);
            }
            else
            {
                ScreenManager.Instance.DeactivateScreen();
                CursorState(false);
            }
                
        }

        if (ScreenManager.Instance.IfScreenActive(_pauseScreen))
            return;

        if (_controller.IsTakedObject)
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
        if(_isDead || ScreenManager.Instance.IfScreenActive(_pauseScreen)) return;

        _movement.Movement(_controller.InputDirection, _controller.IsSprinting);
    }

    public void InstanceKill()
    {
        _rb.freezeRotation = false;
        _rb.AddForce(Vector3.forward * _forceImpulse, ForceMode.Impulse);
        Invoke(nameof(ActivateLooseScreen), _delayToActivatePause);
        _isDead = true;
    }

    void ActivateLooseScreen()
    {
        ScreenManager.Instance.ActivateScreen(_loseScreen);
        CursorState(true);
    }

    void CursorState(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}