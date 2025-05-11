using UnityEngine;

public class BaseMonster : MonoBehaviour, IEnemy
{
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public bool CanAttack()
    {
        throw new System.NotImplementedException();
    }

    public bool CanMove()
    {
        throw new System.NotImplementedException();
    }

    public void Damage()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Move(float direction)
    {
        throw new System.NotImplementedException();
    }

    public void Wait()
    {
        throw new System.NotImplementedException();
    }
}
