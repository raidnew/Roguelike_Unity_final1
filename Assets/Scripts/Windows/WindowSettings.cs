using System;
using UnityEngine;

public class WindowSettings : WindowBase
{
    static public Action ClickBack;

    [SerializeField] private Button _backButton;

    override protected void InitWindow()
    {
        _backButton.Click += OnClick;
    }

    override protected void DeinitWindow()
    {
        _backButton.Click -= OnClick;
    }

    private void OnClick()
    {
        ClickBack?.Invoke();
        Close();
    }
}
