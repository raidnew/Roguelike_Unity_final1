using TMPro;
using UnityEngine;

public class ProgressValue : MonoBehaviour
{
    [SerializeField] private PropertyProvider _propertyProvider;
    [SerializeField] private TextMeshProUGUI _progressValues;

    private void Awake()
    {
        _propertyProvider.SetValue += OnSetValue;
    }

    private void OnSetValue(int value, int max)
    {
        _progressValues.text = value.ToString() + "/" + max.ToString();
    }
}
