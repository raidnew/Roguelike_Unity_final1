using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
public class BrokenObject : MonoBehaviour
{
    public Action OnDied;

    public float Percent => _health.CurrentPercent;
    public float Value => _health.CurrentHealth;

    private Health _health;
    void Awake()
    {
        InitHealth();
    }

    public void InitHealth()
    {
        _health = GetComponent<Health>();
        _health.OnDied += Die;
    }

    public virtual void Die()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        OnDied?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IWeapon weapon;
        if (collision.gameObject.TryGetComponent<IWeapon>(out weapon))
            _health.Damage(weapon.DamageValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IWeapon weapon;
        if (collision.gameObject.TryGetComponent<IWeapon>(out weapon))
            _health.Damage(weapon.DamageValue);
    }


}
