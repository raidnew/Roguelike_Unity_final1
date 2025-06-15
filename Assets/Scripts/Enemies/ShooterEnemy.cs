using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class ShooterEnemy : Enemy
{
    [SerializeField] private float _timeBeetweenAttack;
    [SerializeField] private Transform _barretPoint;
    [SerializeField] private EnemyBullet _bulletPrefab;
    [SerializeField] private float _bulletSpeed;

    public static Action<EnemyBullet> Shot;

    private Animator _animator;
    private Rigidbody2D _rb;
    private double _lastAttackTime;
    private bool _isAttack;
    private Vector3 _targetPosition;

    private bool IsAttack
    {
        get { return _isAttack; }
        set
        {
            _animator.SetBool("IsShot", value);
            _isAttack = value;
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    override public void Attack(Vector3 target)
    {
        _targetPosition = target + Vector3.zero;
        IsAttack = true;
    }

    override public bool CanAttack()
    {
        return !IsAttack && Time.time > _lastAttackTime + _timeBeetweenAttack;
    }

    override public bool CanMove() => false;

    override public void Die()
    {
        _animator.SetBool("IsAlive", false);
    }

    override public void Move(float direction)
    {
        
    }

    override public void Wait()
    {
        
    }

    private void OnFinishDieAnimation()
    {

    }

    private void OnShotAnimation()
    {
        EnemyBullet bullet = Instantiate<EnemyBullet>(_bulletPrefab, _barretPoint.position, _barretPoint.rotation);

        Vector3 directionShot = (_targetPosition - _barretPoint.position);
        directionShot.Normalize();
        directionShot *= _bulletSpeed;

        bullet.AddForce(directionShot);

        Shot?.Invoke(bullet);
    }

    private void OnFinishShotAnimation()
    {
        _animator.SetBool("IsShot", false);
        _lastAttackTime = Time.time;
        IsAttack = false;
    }
}
