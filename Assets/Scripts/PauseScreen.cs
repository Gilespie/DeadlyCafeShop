using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour, IScreen
{
    [SerializeField] GameObject _root;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _returnToMenuButton;
    [SerializeField] Button _exitGameButton;

    void Start()
    {
        _resumeButton.onClick.AddListener(() => ScreenManager.Instance.DeactivateScreen());
        _restartButton.onClick.AddListener(RestartLevel);
        _returnToMenuButton.onClick.AddListener(ReturnToMenu);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    public void Activate()
    {
        PauseManager.Instance.Pause(this);
        _root.SetActive(true);
    }

    public void Deactivate()
    {
        PauseManager.Instance.Unpause(this);
        _root.SetActive(false);
    }

    public void ReturnToMenu()
    {
        ScreenManager.Instance.DeactivateScreen();
        SceneTransition.Instance.LoadLevel("MainMenu");
    }

    public void RestartLevel()
    {
        ScreenManager.Instance.DeactivateScreen();
        SceneTransition.Instance.RestartLevel();
    }

    public void ExitGame()
    {
        ScreenManager.Instance.DeactivateScreen();
        SceneTransition.Instance.ExitFromGame();
    }
}