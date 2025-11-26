using UnityEngine;
using UnityEngine.UI;

public class ControlScreen : MonoBehaviour, IScreen
{
    [SerializeField] GameObject _root;
    [SerializeField] Button _settingsCloseButton;

    void Start()
    {
        _settingsCloseButton.onClick.AddListener(() => ScreenManager.Instance.DeactivateScreen());
    }

    public void Activate()
    {
        _root.SetActive(true);
    }

    public void Deactivate()
    {
        _root.SetActive(false);
    }
}