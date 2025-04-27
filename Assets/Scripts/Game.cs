using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private WindowsManager _windowsManager;
    void Start()
    {
        _windowsManager.OpenWindowMain();
    }

    void Update()
    {
        
    }
}
