using System;
using UnityEngine;

public class WindowWin : WindowBase
{
    static public Action ClickWin;

    [SerializeField] private Button _winButton;

    override protected void InitWindow()
    {
        _winButton.Click += OnClick;
    }

    override protected void DeinitWindow()
    {
        _winButton.Click -= OnClick;
    }

    private void OnClick()
    {
        ClickWin?.Invoke();
        Close();
    }
}
