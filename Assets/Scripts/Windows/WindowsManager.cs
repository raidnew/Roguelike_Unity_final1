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

    private static List<WindowBase> _openedWindows;

    private void Awake()
    {
        if (_openedWindows == null)
        {
            _openedWindows = new List<WindowBase>();
            InitEvents();
        }
    }

    private void InitEvents()
    {
        WindowBase.windowOpened += OnWindowOpen;
        WindowBase.windowClosed += OnWindowClose;
    }

    private void DeinitEvents()
    {
        WindowBase.windowOpened -= OnWindowOpen;
        WindowBase.windowClosed -= OnWindowClose;
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
        CheckBackground();
    }

    public void CloseAllWindows()
    {
        while (_openedWindows.Count > 0)
            _openedWindows[0].Close();
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
