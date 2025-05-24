using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    public virtual void Attack() => throw new System.NotImplementedException();

    public virtual bool CanAttack() => throw new System.NotImplementedException();

    public virtual bool CanMove() => throw new System.NotImplementedException();

    public virtual void Move(float direction) => throw new System.NotImplementedException();

    public virtual void Wait() => throw new System.NotImplementedException();

    public virtual void Die() => throw new System.NotImplementedException();

    public void Damage() => throw new System.NotImplementedException();

    protected void Flip(bool isFlip)
    {
        transform.localScale = new Vector3((isFlip ? 1 : -1), 1, 1);
    }

    
}

