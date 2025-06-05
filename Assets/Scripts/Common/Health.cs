using System;
using UnityEngine;

public class Health : PropertyProvider
{
    public Action OnDied;
    public Action SetHealth;

    [SerializeField] private float _maxHealth;

    private float _currentHealth;
    private bool _isAlive;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        private set 
        {
            _currentHealth = value;
            if (_currentHealth < 0) _currentHealth = 0;
            if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
            CheckAlive();
            SetHealthBar();
        }
    }

    public float CurrentPercent
    {
        get { return CurrentHealth / _maxHealth; }
    }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _isAlive = true;
    }

    public void Damage(float damageValue)
    {
        CurrentHealth = CurrentHealth - damageValue;
    }

    public void Repair(float repairValue)
    {
        CurrentHealth = CurrentHealth + repairValue;
    }

    private void SetHealthBar()
    {
        SetPercent?.Invoke(CurrentHealth / _maxHealth);
        SetValue?.Invoke((int)CurrentHealth, (int)_maxHealth);
    }

    private void CheckAlive()
    {
        if (CurrentHealth <= 0 && _isAlive)
        {
            _isAlive = false;
            OnDied?.Invoke();
            Finish?.Invoke();
        }
    }
}
