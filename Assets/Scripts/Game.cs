using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WindowsManager _windowsManager;
    private static int _startCount = 0;

    private void Start()
    {
        if (_startCount++ == 0)
            Init();
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
        Debug.Log("OnLevelFailed");
        SceneManager.LoadScene("Main");
        _windowsManager.OpenWindowDied();
    }

    private void StartLevel()
    {
        _startCount++;
        SceneManager.LoadScene("Level1");
    }
}
