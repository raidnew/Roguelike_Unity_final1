using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Action Click;

    [SerializeField] private GameObject _normalView;
    [SerializeField] private GameObject _hoverView;
    [SerializeField] private GameObject _pushView;
    [SerializeField] private GameObject _disableView;
    [SerializeField] private GameObject _textField;
    [SerializeField] private ButtonsHitArea _hitArea;
    //[SerializeField] private bool isEnable = true;

    void Start()
    {
        Init();
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
        _hoverView.SetActive(true);
        _normalView.SetActive(false);
    }

    private void OnButtonOut()
    {
        _hoverView.SetActive(false);
        _normalView.SetActive(true);
    }

    private void OnButtonClick()
    {
        Click?.Invoke();
        _normalView.SetActive(false);
        _pushView.SetActive(true);
    }

    private void Init()
    {
        _normalView.SetActive(true);
        _hoverView.SetActive(false);
        _pushView.SetActive(false);
        _disableView.SetActive(false);
        InitButton();
    }

    void Update()
    {
        
    }
}
