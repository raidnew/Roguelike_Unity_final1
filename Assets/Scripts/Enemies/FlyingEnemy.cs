using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class FlyingEnemy : Enemy
{
    [SerializeField] private int _baseSpeed;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override bool CanMove() => true;

    public override void Move(float direction) 
    {
        SetHSpeed(direction * _baseSpeed);
    }

    private void SetHSpeed(float speed)
    {
        if (speed != 0) Flip(speed < 0);
        _rb.linearVelocity = new Vector2(speed, 0);
    }

}
