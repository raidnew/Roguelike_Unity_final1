using System;
using UnityEngine;

public class WindowDied : WindowBase
{
    static public Action ClickReplay;

    [SerializeField] private Button _replayButton;

    override protected void InitWindow()
    {
        _replayButton.Click += OnPlayClick;
    }

    override protected void DeinitWindow()
    {
        _replayButton.Click -= OnPlayClick;
    }

    private void OnPlayClick()
    {
        ClickReplay?.Invoke();
        Close();
    }
}
