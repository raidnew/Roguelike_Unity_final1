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

    [SerializeField] private IconData[] _icons;
    [SerializeField] private GameObject _textField;
    [SerializeField] private ButtonsHitArea _hitArea;

    private int _currentState = 0;

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
        if (_icons[_currentState]._hoverView)
        {
            _icons[_currentState]._normalView.SetActive(false);
            _icons[_currentState]._hoverView.SetActive(true);
        }
    }

    private void OnButtonOut()
    {
        if (_icons[_currentState]._hoverView) 
        {
            _icons[_currentState]._normalView.SetActive(true);
            _icons[_currentState]._hoverView.SetActive(false);
        }
    }

    private void OnButtonClick()
    {
        _currentState = (_currentState + 1) % _icons.Length;
        DisableAllIcons();
        EnableCurrentIcon();
        Toggle?.Invoke(_currentState);
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
        _icons[_currentState]._normalView.SetActive(true);
        if (_icons[_currentState]._hoverView) _icons[_currentState]._hoverView.SetActive(false);
        if (_icons[_currentState]._pushView) _icons[_currentState]._pushView.SetActive(false);
        if (_icons[_currentState]._disableView) _icons[_currentState]._disableView.SetActive(false);
    }

    private void Init()
    {
        DisableAllIcons();
        EnableCurrentIcon();
        InitButton();
    }

}
