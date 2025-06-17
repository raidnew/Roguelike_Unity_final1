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
    private bool _hasConsumer = true;

    public float MaxReserve { get => _maxReserve; }
    public float CurrentReserve { get => _reserve; }


    private void Awake()
    {
        _maxReserve = _reserve = _startReserve + Level.CountStarts * _reserveOnEveryStart;
    }

    private void OnEnable()
    {
        Level.LevelEnd += OnLevelEnd;
    }

    private void OnDisable()
    {
        Level.LevelEnd -= OnLevelEnd;
    }

    private void Update()
    {
        if (_reserve > 0 && _hasConsumer)
        {
            _reserve -= _expendeture * Time.deltaTime;
            SetPercent?.Invoke(_reserve / _maxReserve);
            SetValue?.Invoke((int)_reserve, (int)_maxReserve); 
            if (_reserve <= 0) GetEmpty?.Invoke();
        }
    }

    private void OnLevelEnd()
    {
        _hasConsumer = false;
    }
}
