using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IWeapon
{
    public float DamageValue => 10;

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 speed)
    {
        _rb.AddForce(speed);
    }
}
