using System;
using UnityEngine;

public class AstronautAnimationEvents : MonoBehaviour
{
    public Action OnFinishAttack;
    public Action OnFinishShoot;
    public Action OnFinishDieAnimation;
    public Action Bullet;

    private void AttackFinish()
    {
        OnFinishAttack?.Invoke();
    }

    private void ShootFinish()
    {
        OnFinishShoot?.Invoke();
    }

    private void DieFinish()
    {
        OnFinishDieAnimation?.Invoke();
    }

    private void Shoot()
    {
        Bullet?.Invoke();
    }
}
