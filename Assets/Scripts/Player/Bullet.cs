using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IWeapon
{
    [SerializeField] float _timeToLive;
    public float DamageValue => 10;

    private Rigidbody2D _rb;
    private float _bornTime;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bornTime = Time.time;
    }

    private void Update()
    {
        if(_bornTime + _timeToLive < Time.time)
            Destroy(gameObject);
    }

    public void AddForce(Vector2 speed)
    {
        _rb.AddForce(speed);
    }
}
