using System;
using UnityEngine;

public class Oxigen : PropertyProvider
{
    public Action GetEmpty;

    [SerializeField] private float _startReserve = 10;
    [SerializeField] private float _reserveOnEveryStart = 1.1f;

    private float _maxReserve = 0;
    private float _reserve = 0;
    private float _expendeture = 1;

    private void Awake()
    {
        _maxReserve = _reserve = _startReserve + Level.CountStarts * _reserveOnEveryStart;
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
