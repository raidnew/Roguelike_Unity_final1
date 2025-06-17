using System;
using UnityEngine;

[Serializable]
class IconData
{
    [SerializeField] public GameObject _normalView;
    [SerializeField] public GameObject _hoverView;
    [SerializeField] public GameObject _pushView;
    [SerializeField] public GameObject _disableView;
}

public class ToggleButton : MonoBehaviour
{
    public Action<int> Toggle;

    [SerializeField] private String _storageName;
    [SerializeField] private IconData[] _icons;
    [SerializeField] private GameObject _textField;
    [SerializeField] private ButtonsHitArea _hitArea;

    public int CurrentState
    {
        get
        {
            return Settings.Load(_storageName);
        }
        private set
        {
            Settings.Save(_storageName, value);
        }
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        DeinitButton();
    }

    private void InitButton()
    {
        _hitArea.OnClick += OnButtonClick;
        _hitArea.OnOut += OnButtonOut;
        _hitArea.OnOver += OnButtonOver;
    }

    private void DeinitButton()
    {
        _hitArea.OnClick -= OnButtonClick;
        _hitArea.OnOut -= OnButtonOut;
        _hitArea.OnOver -= OnButtonOver;
    }

    private void OnButtonOver()
    {
        if (_icons[CurrentState]._hoverView)
        {
            _icons[CurrentState]._normalView.SetActive(false);
            _icons[CurrentState]._hoverView.SetActive(true);
        }
    }

    private void OnButtonOut()
    {
        if (_icons[CurrentState]._hoverView) 
        {
            _icons[CurrentState]._normalView.SetActive(true);
            _icons[CurrentState]._hoverView.SetActive(false);
        }
    }

    private void OnButtonClick()
    {
        CurrentState = (CurrentState + 1) % _icons.Length;
        DisableAllIcons();
        EnableCurrentIcon();
        Toggle?.Invoke(CurrentState);
    }

    private void DisableAllIcons()
    {
        foreach (var icon in _icons)
        {
            icon._normalView.SetActive(false);
            if (icon._hoverView) icon._hoverView.SetActive(false);
            if (icon._pushView) icon._pushView.SetActive(false);
            if (icon._disableView) icon._disableView.SetActive(false);
        }
    }

    private void EnableCurrentIcon()
    {
        _icons[CurrentState]._normalView.SetActive(true);
        if (_icons[CurrentState]._hoverView) _icons[CurrentState]._hoverView.SetActive(false);
        if (_icons[CurrentState]._pushView) _icons[CurrentState]._pushView.SetActive(false);
        if (_icons[CurrentState]._disableView) _icons[CurrentState]._disableView.SetActive(false);
    }

    private void Init()
    {
        DisableAllIcons();
        EnableCurrentIcon();
        Toggle?.Invoke(CurrentState);
        InitButton();
    }

}
