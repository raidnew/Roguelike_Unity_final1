using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WindowsManager _windowsManager;
    private static bool _firstInit = true;

    private void Start()
    {
        if (_firstInit)
        {
            _firstInit = false;
            Init();
        }
    }

    private void Init()
    {
        WindowMain.PlayClick += StartLevel;
        WindowMain.HelpClick += OnHelpClick;
        WindowMain.SettingsClick += OnSettingsClick;
        WindowDied.ClickReplay += StartLevel;
        WindowHelp.ClickBack += MainMenu;
        WindowSettings.ClickBack += MainMenu;
        WindowWin.ClickWin += MainMenu;
        Level.LevelFailed += OnLevelFailed;
        Level.LevelWin += OnLevelWin;
        Level.LevelQuit += OnLevelQuit;
        _windowsManager.OpenWindowMain();
    }

    private void MainMenu()
    {
        _windowsManager.OpenWindowMain();
    }
    
    private void OnLevelWin()
    {
        SceneManager.LoadScene("Main");
        _windowsManager.OpenWindowWin();
    }

    private void OnLevelFailed()
    {
        SceneManager.LoadScene("Main");
        _windowsManager.OpenWindowDied();
    }

    private void OnLevelQuit()
    {
        SceneManager.LoadScene("Main");
        _windowsManager.OpenWindowMain();
    }

    private void StartLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OnHelpClick()
    {
        _windowsManager.OpenWindowHelp();
    }

    private void OnSettingsClick()
    {
        _windowsManager.OpenWindowSetings();
    }
}
