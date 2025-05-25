using System;
using UnityEngine;

public class Oxigen : PropertyProvider
{
    public Action GetEmpty;

    private float _maxReserve = 10;
    private float _reserve = 0;
    private float _expendeture = 1;

    private void Awake()
    {
        _reserve = _maxReserve;
    }

    private void Update()
    {
        if (_reserve > 0)
        {
            _reserve -= _expendeture * Time.deltaTime;
            SetValue(_reserve / _maxReserve);
            if (_reserve <= 0) GetEmpty?.Invoke();
        }
    }
}
