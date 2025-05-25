using System;
using UnityEngine;

public class WindowDied : WindowBase
{
    static public Action ClickReplay;

    [SerializeField] private Button _replayButton;

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
        _replayButton.Click += OnPlayClick;
    }

    private void OnPlayClick()
    {
        ClickReplay?.Invoke();
        Close();
    }
}
