using UnityEditor.SearchService;
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
        WindowDied.ClickReplay += StartLevel;
        Level.LevelFailed += OnLevelFailed;
        _windowsManager.OpenWindowMain();
    }

    private void OnLevelFailed()
    {
        SceneManager.LoadScene("Main");
        _windowsManager.OpenWindowDied();
    }

    private void StartLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
