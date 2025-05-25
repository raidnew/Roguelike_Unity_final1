using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Astronaut : MonoBehaviour, IInputListener, IPlayer
{
    public Action Die;
    public Action Shot;

    [SerializeField] private Transform _shootPoint;
    [Header("Movement settings")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _airHorizontalSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _pushPower;
    [SerializeField] private Transform _bubbleLink;
    [SerializeField] private AstronautAnimation _playerAnimator;
    [SerializeField] private AstronautAnimationEvents _animationEvents;
    [SerializeField] private Oxigen _oxigenStorage;

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

    private bool IsAllowSetHSpeed
    {
        get { return _health.CurrentPercent > 0 && !_isAttack; }
    }

    private bool IsAllowSetVSpeed
    {
        get { return _health.CurrentPercent > 0 && !_isAttack && IsOnGround; }
    }

    private bool IsAllowShoot
    {
        get { return _health.CurrentPercent > 0 && !_isShoot && IsOnGround; }
    }

    public void Move(float value)
    {
        float currentHorizontalSpeed = 0;
        currentHorizontalSpeed = IsOnGround? _walkSpeed : _airHorizontalSpeed;
        currentHorizontalSpeed *= value;
        SetHSpeed(currentHorizontalSpeed);
    }

    public void Jump()
    {
        SetVSpeed(_jumpPower);
    }

    public void Attack()
    {
        /*
        if (IsAllowAttack)
        {
            _isAttack = true;
            SetHSpeed(0);
            _playerAnimator.StartAttack();
        }
        */
    }

    private void FinishAttack()
    {
        //_isAttack = false;
    }

    public void Shoot()
    {
        if (IsAllowShoot)
        {
            _isShoot = true;
            SetHSpeed(0);
            _playerAnimator.StartShoot();
        }
    }

    private void FinishShoot()
    {
        _isShoot = false;
    }

    private void Awake()
    {
        _playerRigitBody2D = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _health.OnDied += StartDie;
        _oxigenStorage.GetEmpty += OnOxigetEmpty;

        _animationEvents.OnFinishAttack += FinishAttack;
        _animationEvents.OnFinishShoot += FinishShoot;
        _animationEvents.OnFinishDieAnimation += OnDieFinish;
    }

    private void FixedUpdate()
    {
        SetupAnimationSpeed();
    }

    private void OnOxigetEmpty()
    {
        _health.Damage(_health.CurrentHealth);
    }

    private void SetTouchGround(IGround ground, bool touch)
    {
        if (touch)
            _groundAreTouching.Add(ground);
        else if (_groundAreTouching.Contains(ground))
            _groundAreTouching.Remove(ground);

        _playerAnimator.OnGround(IsOnGround);
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
        _playerAnimator.SetHorizontalSpeed(relativeSpeed.x);
        _playerAnimator.SetVerticalSpeed(relativeSpeed.y);
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

    private void OnShoot()
    {
        //IsShoot = false;
        CreateBullet();
    }

    private void StartDie()
    {
        _playerAnimator.StartDie();
    }

    private void OnDieFinish()
    {
        Die?.Invoke();
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
