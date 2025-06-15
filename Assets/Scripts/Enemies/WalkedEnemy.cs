using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class WalkedEnemy : Enemy
{
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private float _timeBeetweenAttack;
    [SerializeField] private int _baseSpeed;

    private Animator _animator;
    private Rigidbody2D _rb;
    private bool _isAttack;
    private double _lastAttackTime;

    private bool IsAttack
    {
        get { return _isAttack; }
        set
        {
            _animator.SetBool("IsAttack", value);
            _isAttack = value;
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public override bool CanAttack()
    {
        return !IsAttack && Time.time > _lastAttackTime + _timeBeetweenAttack;
    }

    public override bool CanMove()
    {
        return true;
    }

    public override void Move(float direction)
    {
        SetHSpeed((int)(direction * _baseSpeed));
    }

    public override void Wait()
    {
        SetHSpeed(0);
    }

    public override void Attack(Vector3 target) => IsAttack = true;

    public override void Die()
    {
        _animator.SetBool("IsAlive", false);
    }

    private void SetHSpeed(float speed)
    {
        if (speed != 0) Flip(speed < 0);
        _rb.linearVelocity = new Vector2(speed, _rb.linearVelocity.y);
        _animator.SetInteger("HSpeed", (int)speed);
    }

    private void Hit()
    {
        _weapon.StartHit();
    }

    private void OnFinishAttack()
    {
        _weapon.StopHit();
        IsAttack = false;
        _lastAttackTime = Time.time;
    }

}
