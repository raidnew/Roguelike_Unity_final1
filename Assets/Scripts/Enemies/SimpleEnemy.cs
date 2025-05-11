using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] private BrokenObject _life;

    void Awake()
    {
        _life.OnDied += Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
