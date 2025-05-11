using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    [SerializeField] private WindowMain _windowMain;
    [SerializeField] private WindowWin _windowWin;
    [SerializeField] private WindowDied _windowDied;
    [SerializeField] private GameObject _tintArea;
    [SerializeField] private GameObject _backgroungImage;

    private List<WindowBase> _openedWindows;

    private void Awake()
    {
        CloseAllWindows();
        _openedWindows = new List<WindowBase>();
        WindowBase.windowOpened += OnWindowOpen;
        WindowBase.windowClosed += OnWindowClose;
    }

    private void CheckBackground()
    {
        if (_openedWindows.Count > 0)
        {
            _backgroungImage.SetActive(true);
            _tintArea.SetActive(true);
        }
        else
        {
            _backgroungImage.SetActive(false);
            _tintArea.SetActive(false);
        }
    }

    private void OnWindowOpen(WindowBase window)
    {
        _openedWindows.Add(window);
        CheckBackground();
    }

    private void OnWindowClose(WindowBase window)
    {
        _openedWindows.Remove(window);
        CheckBackground();
    }

    public void CloseAllWindows()
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
