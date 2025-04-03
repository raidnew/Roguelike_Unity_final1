public interface IEnemy
{
    void Move(float direction);
    void Attack();
    void Wait();
    void Die();
    bool CanAttack();
    bool CanMove();

}
