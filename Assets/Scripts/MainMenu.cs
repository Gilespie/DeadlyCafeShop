using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _controlScreen;
    [Header("Buttons")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _controlButton;
    [SerializeField] Button _exitButton;

    void Start()
    {
        _playButton.onClick.AddListener(() => SceneTransition.Instance.LoadLevel("Level"));
        _controlButton.onClick.AddListener(() => ScreenManager.Instance.ActivateScreen(_controlScreen));
        _exitButton.onClick.AddListener(() => SceneTransition.Instance.ExitFromGame());
    }
}