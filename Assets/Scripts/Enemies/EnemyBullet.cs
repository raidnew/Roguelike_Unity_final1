using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour, IDamager
{
    [SerializeField] float _timeToLive;
    public float Damage => 15;

    private Rigidbody2D _rb;
    private float _bornTime;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bornTime = Time.time;
    }

    private void Update()
    {
        if (_bornTime + _timeToLive < Time.time)
            Destroy(gameObject);
    }
    public void AddForce(Vector2 speed)
    {
        _rb.AddForce(speed);
    }
}
