using System;
using UnityEngine;

public class WindowMain : WindowBase
{
    static public Action PlayClick;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnHelp;
    [SerializeField] private Button _btnSetting;

    private void Awake()
    {
        _btnPlay.Click += OnPlayClick;
    }

    private void OnPlayClick()
    {
        PlayClick?.Invoke();
    }
}
