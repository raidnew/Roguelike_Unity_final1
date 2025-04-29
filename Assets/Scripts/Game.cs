using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WindowsManager _windowsManager;
    
    void Start()
    {
        _windowsManager.OpenWindowMain();
        WindowMain.PlayClick += StartLevel;
        Level.LevelFailed += OnLevelFailed;
    }

    void Update()
    {
        
    }

    private void OnLevelFailed()
    {
        Debug.Log("fdsfsdaf");
        SceneManager.LoadScene("Main");
    }

    private void StartLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
