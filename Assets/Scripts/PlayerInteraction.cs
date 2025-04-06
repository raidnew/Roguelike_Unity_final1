using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class PlayerInteraction : MonoBehaviour, IInputListener, IPlayer
{
    public Action OnDied;

    [SerializeField] private Transform _shootPoint;
    [Header("Movement settings")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _airHorizontalSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _pushPower;
    [SerializeField] private Transform _bubbleLink;
    [SerializeField] private Animator _playerAnimator;

    private bool _isRun = false;
    private bool _isAttack = false;
    private bool _isShoot = false;
    private Rigidbody2D _playerRigitBody2D;
    private Health _health;

    private List<IGround> _groundAreTouching = new List<IGround>();

    private IGround CurrentGround{
        get {
            if (_groundAreTouching.Count == 0) return null;
            return _groundAreTouching.Last();
        }
    }

    private bool _isFlip = false;

    private bool IsFlip
    {
        get => _isFlip;
        set
        {
            _isFlip = value;
            transform.localScale = new Vector3((value ? 1 : -1), 1, 1);
        }
    }

    private bool IsOnGround
    {
        get => CurrentGround != null;
    }

    private Vector2 _lastGroundSpeed = Vector2.zero;
    private Vector2 LastGroundSpeed 
    {
        get
        {
            if (IsOnGround) _lastGroundSpeed = CurrentGround.Speed;
            return _lastGroundSpeed;
        }
    }

    private bool IsAttack
    {
        get { return _isAttack; }
        set
        {
            _isAttack = value;
            _playerAnimator.SetBool("IsAttack", _isAttack);
        }
    }
    private bool IsShoot
    {
        get { return _isShoot; }
        set
        {
            _isShoot = value;
            _playerAnimator.SetBool("IsShoot", _isShoot);
        }
    }

    private bool IsRun
    {
        get { return _isRun; }
        set
        {
            _isRun = value;
            _playerAnimator.SetBool("IsRun", _isRun);
        }
    }

    private bool IsAllowSetHSpeed
    {
        get { return _health.CurrentPercent > 0 && !_isAttack; }
    }

    private bool IsAllowSetVSpeed
    {
        get { return _health.CurrentPercent > 0 && !_isAttack && IsOnGround; }
    }

    private bool IsAllowAttack
    {
        get { return _health.CurrentPercent > 0 && !_isAttack && IsOnGround; }
    }

    public void Move(float value)
    {
        float currentHorizontalSpeed = 0;
        if (!IsOnGround) currentHorizontalSpeed = _airHorizontalSpeed;
        else currentHorizontalSpeed = (IsRun ? _runSpeed : _walkSpeed);
        currentHorizontalSpeed *= value;
        SetHSpeed(currentHorizontalSpeed);
    }

    public void Run(bool isRun)
    {
        IsRun = isRun;
    }

    public void Jump()
    {
        SetVSpeed(_jumpPower);
    }

    public void Attack()
    {
        if (IsAllowAttack)
        {
            SetHSpeed(0);
            IsAttack = true;
        }
    }

    public void Shoot()
    {
        if (IsAllowAttack)
        {
            SetHSpeed(0);
            IsShoot = true;
        }
    }

    private void Awake()
    {
        _playerRigitBody2D = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _health.OnDied += Die;
        IsFlip = true;
    }

    private void FixedUpdate()
    {
        SetupAnimationSpeed();
    }

    private void SetTouchGround(IGround ground, bool touch)
    {
        if (touch)
            _groundAreTouching.Add(ground);
        else if (_groundAreTouching.Contains(ground))
            _groundAreTouching.Remove(ground);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IGround ground;
        IPlayer player;
        bool isPlayer = collision.otherCollider.gameObject.TryGetComponent<IPlayer>(out player);
        bool isGround = collision.gameObject.TryGetComponent<IGround>(out ground);
        if (isGround && isPlayer)
            SetTouchGround(ground, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IGround ground;
        IPlayer player;
        bool isPlayer = collision.otherCollider.gameObject.TryGetComponent<IPlayer>(out player);
        bool isGround = collision.gameObject.TryGetComponent<IGround>(out ground);
        if (isGround && isPlayer)
            SetTouchGround(ground, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamager damager;
        if (collision.gameObject.TryGetComponent<IDamager>(out damager))
            _health.Damage(damager.Damage);
        IDiedArea diedArea;
        if (collision.gameObject.TryGetComponent<IDiedArea>(out diedArea))
            _health.Damage(_health.CurrentHealth);
        IHealer healer;
        if (collision.gameObject.TryGetComponent<IHealer>(out healer))
            _health.Repair(_health.CurrentHealth);

    }

    private void SetupAnimationSpeed()
    {
        Vector2 relativeSpeed = _playerRigitBody2D.linearVelocity;
        if (IsOnGround) relativeSpeed -= CurrentGround.Speed;

        if (relativeSpeed.x < -0.5)
            IsFlip = false;
        else if (relativeSpeed.x > 0.5)
            IsFlip = true;

        SetVerticalSpeed((int)(relativeSpeed.y * 10));
        _playerAnimator.SetInteger("HSpeed", (int)(relativeSpeed.x * 10));
    }

    private void SetVerticalSpeed(int verticalSpeed)
    {
        _playerAnimator.SetInteger("VSpeed", verticalSpeed);
    }

    private void CreateBullet()
    {
        //Bullet bullet = Instantiate<Bullet>(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        //bullet.Flip(IsFlip);
        //OnCreateBullet?.Invoke(bullet);
    }

    private void OnDamage()
    {
        //_weapon.StartHit();
    }

    private void OnFinishAttack()
    {
        //_weapon.StopHit();
        IsAttack = false;
    }

    private void OnShoot()
    {
        IsShoot = false;
        CreateBullet();
    }

    private void Die()
    {
        _playerAnimator.SetBool("IsAlive", _health.CurrentPercent > 0);
    }

    private void OnFinishDie()
    {
        OnDied?.Invoke();
    }

    private void SetHSpeed(float speed)
    {
        if (IsAllowSetHSpeed)
        {
            speed += LastGroundSpeed.x;
            _playerRigitBody2D.linearVelocity = new Vector2(speed, _playerRigitBody2D.linearVelocity.y);
        }
    }

    private void SetVSpeed(float speed) 
    {
        if (IsAllowSetVSpeed)
        {
            speed += LastGroundSpeed.y;
            _playerRigitBody2D.linearVelocity = new Vector2(_playerRigitBody2D.linearVelocity.x, speed);
        }
    }
}
