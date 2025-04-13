using System;
using UnityEngine;

public class AstronautAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AstronautAnimationEvents _animatorEvents;

    public Action OnShoot;
    public Action OnKick;

    private void Awake()
    {
        _animatorEvents.OnFinishShoot += StopShoot;
        _animatorEvents.OnFinishAttack += StopAttack;
    }

    public void StartAttack()
    {
        _animator.SetBool("IsAttack", true);
    }
    
    public void StartShoot()
    {
        _animator.SetBool("IsShoot", true);
    }

    public void OnGround(bool onGround)
    {
        _animator.SetBool("OnGround", onGround);
    }

    public void StartDie()
    {
        _animator.SetBool("IsAlive", true);
    }

    public void SetVerticalSpeed(float speed)
    {
        _animator.SetInteger("VSpeed", (int)(speed * 10));
    }

    public void SetHorizontalSpeed(float speed)
    {
        Vector3 scale = transform.localScale + Vector3.zero;

        if (speed < -0.5)
            scale.x = -Math.Abs(scale.x);
        else if (speed > 0.5)
            scale.x = Math.Abs(scale.x);
    
        transform.localScale = scale;

        _animator.SetInteger("HSpeed", (int)(speed * 10));
    }

    private void StopShoot()
    {
        _animator.SetBool("IsShoot", false);
    }

    private void StopAttack()
    {
        _animator.SetBool("IsAttack", false);
    }

}
