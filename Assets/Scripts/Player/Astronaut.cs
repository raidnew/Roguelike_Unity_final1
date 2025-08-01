using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Astronaut : MonoBehaviour, IInputListener, IPlayer
{
    public Action Die;
    public Action<Bullet> BeginBullet;

    [SerializeField] private Transform _shootPoint;
    [Header("Movement settings")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _airHorizontalSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _pushPower;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bubbleLink;
    [SerializeField] private AstronautAnimation _playerAnimator;
    [SerializeField] private AstronautAnimationEvents _animationEvents;
    [SerializeField] private Oxigen _oxigenStorage;
    [SerializeField] private Transform _tellerPoint;
    [SerializeField] private string[] _speeches;

    private bool _isAttack = false;
    private bool _isShoot = false;
    private Rigidbody2D _playerRigitBody2D;
    private Health _health;
    private List<IGround> _groundAreTouching = new List<IGround>();

    private static int _dieCount = 0;

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

    public bool IsFlip { get; private set; }

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
        AudioController.PlaySfxSound(SfxId.PlayerJump);
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
            AudioController.PlaySfxSound(SfxId.PlayerShot);
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
        _animationEvents.Bullet += OnBullet;
        _animationEvents.OnFinishShoot += FinishShoot;
        _animationEvents.OnFinishDieAnimation += OnDieFinish;

        StartCoroutine(GonnaTalk(GetStartSpeech()));
    }

    private string GetStartSpeech()
    {
        if (_speeches.Length == 0) return "I have crushed, i need alive!";
        if (_dieCount < _speeches.Length) return _speeches[_dieCount];
        return _speeches.Last();
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
        if (CheckComponent<IGround>(collision.gameObject))
        {
            IGround ground = collision.gameObject.GetComponent<IGround>();
            SetTouchGround(ground, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckComponent<IGround>(collision.gameObject))
        {
            IGround ground = collision.gameObject.GetComponent<IGround>();
            SetTouchGround(ground, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_health.CurrentHealth <= 0) return;

        IDamager damager;
        if (collision.gameObject.TryGetComponent<IDamager>(out damager)) _health.Damage(damager.Damage);

        if (CheckComponent<IDiedArea>(collision.gameObject)) _health.Damage(_health.CurrentHealth);
        if (CheckComponent<IHealer>(collision.gameObject)) _health.Repair(_health.CurrentHealth);
    }

    private bool CheckComponent<T>(GameObject gameObject)
    {
        T component;
        return gameObject.TryGetComponent<T>(out component);
    }

    private void SetupAnimationSpeed()
    {
        Vector2 relativeSpeed = _playerRigitBody2D.linearVelocity;
        if (IsOnGround) relativeSpeed -= CurrentGround.Speed;
        _playerAnimator.SetHorizontalSpeed(relativeSpeed.x);
        _playerAnimator.SetVerticalSpeed(relativeSpeed.y);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate<Bullet>(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        bullet.AddForce(new Vector2( IsFlip ? -_bulletSpeed : _bulletSpeed, 0));
        return bullet;
    }

    private void OnBullet()
    {
        Bullet bullet = CreateBullet();
        BeginBullet?.Invoke(bullet);
    }

    private void StartDie()
    {
        _playerAnimator.StartDie();
    }

    private void OnDieFinish()
    {
        _dieCount++;
        Die?.Invoke();
    }

    private void SetHSpeed(float speed)
    {
        if (IsAllowSetHSpeed)
        {
            speed += LastGroundSpeed.x;
            _playerRigitBody2D.linearVelocity = new Vector2(speed, _playerRigitBody2D.linearVelocity.y);
            if(Math.Abs(speed) > 0)
                IsFlip = speed < 0;
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

    private IEnumerator GonnaTalk(string message)
    {
        yield return new WaitForSeconds(2);
        Bubble.Message(message, _tellerPoint);
    }
}
