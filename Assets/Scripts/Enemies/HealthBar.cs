using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PropertyProvider _propertyProvider;
    [SerializeField] private GameObject _progressBarImage;

    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private Vector2 _startSize;
    private Vector3 _startPosition;

    private void Awake()
    {
        _propertyProvider.SetValue += OnSetValue;
        _propertyProvider.Finish += OnFinishProvide;
        _spriteRenderer = _progressBarImage.GetComponent<SpriteRenderer>();
        _transform = _progressBarImage.transform;
        _startSize = _spriteRenderer.size + Vector2.zero;
        _startPosition = _transform.localPosition + Vector3.zero;
    }

    private void OnFinishProvide()
    {
        gameObject.SetActive(false);
    }

    private void OnSetValue(float value)
    {
        _spriteRenderer.size = new Vector2(_startSize.x * value, _startSize.y);
        _transform.localPosition = new Vector3(_startPosition.x * value, _startPosition.y, _startPosition.z);
    }
}
