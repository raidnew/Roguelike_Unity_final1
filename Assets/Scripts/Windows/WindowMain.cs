using System;
using UnityEngine;

public class WindowMain : WindowBase
{
    static public Action PlayClick;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnHelp;
    [SerializeField] private Button _btnSetting;

    private static bool _inited = false;

    private void Awake()
    {
        if (!_inited)
        {
            InitWindow();
            _inited = true;
        }
    }

    private void InitWindow()
    {
        _btnPlay.Click += OnPlayClick;
    }

    private void OnPlayClick()
    {
        PlayClick?.Invoke();
        Close();
    }
}
