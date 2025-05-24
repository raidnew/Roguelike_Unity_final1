using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private PropertyProvider _propertyProvider;
    [SerializeField] private Image _progressBarImage;

    private void Awake()
    {
        _propertyProvider.SetValue += OnSetValue;
    }

    private void OnSetValue(float value)
    {
        _progressBarImage.fillAmount = value;
    }
}
