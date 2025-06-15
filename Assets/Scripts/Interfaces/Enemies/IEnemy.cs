using UnityEngine;

public interface IEnemy
{
    void Move(float direction);
    void Attack(Vector3 target);
    void Wait();
    void Die();
    bool CanAttack();
    bool CanMove();
    void Damage();

}
