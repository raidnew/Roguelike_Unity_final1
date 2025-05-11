using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WindowsManager _windowsManager;
    
    void Start()
    {
        WindowMain.PlayClick += StartLevel;
        Level.LevelFailed += OnLevelFailed;
        _windowsManager.OpenWindowMain();
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
        SceneManager.LoadScene("Level1");
    }
}
