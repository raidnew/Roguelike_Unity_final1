using System.Collections.Generic;
using System.Security.Cryptography;
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

    private static bool _inited = false;

    private void Awake()
    {
        if (!_inited)
        {
            CloseAllWindows();
            _openedWindows = new List<WindowBase>();
            WindowBase.windowOpened += OnWindowOpen;
            WindowBase.windowClosed += OnWindowClose;
            _inited = true;
        }
    }

    private void CheckBackground()
    {
        Debug.Log(_openedWindows.Count);
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
        Debug.Log($"open {window}");
        _openedWindows.Add(window);
        CheckBackground();
    }

    private void OnWindowClose(WindowBase window)
    {
        string res = _openedWindows.Remove(window) ? "found" : "notfound";
        Debug.Log($"remove {window} {res}");
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
