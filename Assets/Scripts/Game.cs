using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WindowsManager _windowsManager;
    private static int _startCount = 0;

    void Start()
    {
        WindowMain.PlayClick += StartLevel;
        WindowDied.ClickReplay += StartLevel;
        Level.LevelFailed += OnLevelFailed;
        if (_startCount == 0) _windowsManager.OpenWindowMain();
    }

    void Update()
    {
        
    }

    private void OnLevelFailed()
    {
        SceneManager.LoadScene("Main");
        _windowsManager.OpenWindowDied();
    }

    private void StartLevel()
    {
        _startCount++;
        SceneManager.LoadScene("Level1");
    }
}
