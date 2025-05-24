
using System;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour, IDamager
{
    [SerializeField] private Collider2D _weaponDamageArea;

    public Action AttackHasFinished;
    public Action AttackHasStarted;

    public float Damage => 10;

    public void StartHit()
    {
        _weaponDamageArea.enabled = true;
        AttackHasStarted?.Invoke();
    }

    public void StopHit()
    {
        _weaponDamageArea.enabled = false;
        AttackHasFinished?.Invoke();
    }
}
