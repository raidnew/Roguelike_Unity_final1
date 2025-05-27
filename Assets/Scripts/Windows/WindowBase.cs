using System;
using Unity.VisualScripting;
using UnityEngine;

public class WindowBase : MonoBehaviour, IWindow
{
    public static Action<WindowBase> windowOpened;
    public static Action<WindowBase> windowClosed;

    public void Close()
    {
        windowClosed?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Open()
    {
        windowOpened?.Invoke(this);
        gameObject.SetActive(true);
    }

    virtual protected void InitWindow()
    {

    }

    virtual protected void DeinitWindow()
    {

    }

    private void OnEnable()
    {
        InitWindow();
    }

    private void OnDisable()
    {
        DeinitWindow();
    }


}
