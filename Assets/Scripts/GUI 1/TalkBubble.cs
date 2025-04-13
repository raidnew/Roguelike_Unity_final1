using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TalkBubble : MonoBehaviour, ITalkBubble
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _tile;
    [SerializeField] private TextMeshProUGUI _textField;

    private int _backgroundMargin = 10;
    private bool _needRecalcSizeBackground = true;
    private int _checkSumm;
    private Camera _camera;
    private Transform _linkPoint;
    private RectTransform _rt;

    public Action<ITalkBubble> OnRemoveBubble { get; set; }

    public void ShowMessage(string message, Transform position, Camera camera, int time)
    {
        _rt = GetComponent<RectTransform>();
        _linkPoint = position;
        _camera = camera;
        StartCoroutine(ShowMessage(message, time));
    }

    public int CheckSumm { get; set; }

    private void Update()
    {
        if (_needRecalcSizeBackground) ResizeBackground();
        PositioningBubble();
    }

    private void ResizeBackground()
    {
        Vector3 sizeTextField = _textField.textInfo.textComponent.textBounds.size;
        if (sizeTextField.x > 0)
        {
            _needRecalcSizeBackground = false;
            _background.sizeDelta = new Vector2(sizeTextField.x + _backgroundMargin * 2, sizeTextField.y + _backgroundMargin * 2);
            _tile.localPosition = new Vector3(-3, 78 - (sizeTextField.y + _backgroundMargin * 2) / 2, 0);
        }
    }

    private void PositioningBubble()
    {
        _rt.localPosition = _camera.WorldToScreenPoint(_linkPoint.position);
    }

    private IEnumerator ShowMessage(string message, int time)
    {
        _textField.SetText(message);
        yield return new WaitForSeconds(time);
        OnRemoveBubble?.Invoke(this);
        Destroy(gameObject);
        yield return null;
    }

}
