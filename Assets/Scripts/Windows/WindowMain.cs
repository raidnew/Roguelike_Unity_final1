using System;
using UnityEngine;

public class WindowMain : WindowBase
{
    static public Action PlayClick;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnHelp;
    [SerializeField] private Button _btnSetting;

    override protected void InitWindow()
    {
        _btnPlay.Click += OnPlayClick;
    }

    override protected void DeinitWindow()
    {
        _btnPlay.Click -= OnPlayClick;
    }

    private void OnPlayClick()
    {
        PlayClick?.Invoke();
        Close();
    }
}
