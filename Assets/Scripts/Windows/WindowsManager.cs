using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    [SerializeField] private WindowMain _windowMain;
    [SerializeField] private WindowWin _windowWin;
    [SerializeField] private WindowDied _windowDied;

    private void Awake()
    {
        _windowMain.Close();
        _windowWin.Close();
        _windowDied.Close();
    }

    public void OpenWindowMain()
    {
        _windowMain.Open();
    }

    public void OpenWindowWin()
    {
        _windowWin.Open();
    }

    public void OpenWindowDied()
    {
        _windowDied.Open();
    }
}
