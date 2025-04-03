using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDied;

    [SerializeField] private float _maxHealth;
    //[SerializeField] private ProgressBar _healthBar;

    private float _currentHealth;

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

    public void Damage(float damageValue)
    {
        CurrentHealth = CurrentHealth - damageValue;
    }

    public void Repair(float repairValue)
    {
        CurrentHealth = CurrentHealth + repairValue;
    }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        //_healthBar?.Full();
    }

    private void SetHealthBar()
    {
        //_healthBar?.SetProgress(CurrentHealth / _maxHealth);
    }

    private void CheckAlive()
    {
        if (CurrentHealth <= 0)
            OnDied?.Invoke();
    }
}
