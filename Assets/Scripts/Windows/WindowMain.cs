using System;
using UnityEngine;

public class WindowMain : WindowBase
{
    static public Action PlayClick;
    static public Action HelpClick;
    static public Action SettingsClick;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnHelp;
    [SerializeField] private Button _btnSetting;

    override protected void InitWindow()
    {
        _btnPlay.Click += OnPlayClick;
        _btnHelp.Click += OnHelpClick;
        _btnSetting.Click += OnSettingsClick;
    }

    override protected void DeinitWindow()
    {
        _btnPlay.Click -= OnPlayClick;
        _btnHelp.Click -= OnHelpClick;
        _btnSetting.Click -= OnSettingsClick;
    }

    private void OnPlayClick()
    {
        PlayClick?.Invoke();
        Close();
    }

    private void OnHelpClick()
    {
        HelpClick?.Invoke();
        Close();
    }

    private void OnSettingsClick()
    {
        SettingsClick?.Invoke();
        Close();
    }
}
