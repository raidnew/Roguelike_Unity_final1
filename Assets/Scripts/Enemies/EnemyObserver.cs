using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyObserver : MonoBehaviour, ISight
{
    public Action<Vector3> OnFoundPlayer { get; set; }
    public Action<Vector3> OnWatchPlayer { get; set; }
    public Action<Vector3> OnLostPlayer { get; set; }

    private Transform _playerTransform;

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player;
        if (collision.gameObject.TryGetComponent<IPlayer>(out player))
        {
            OnLostPlayer?.Invoke(_playerTransform.position);
            _playerTransform = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player;
        if (collision.gameObject.TryGetComponent<IPlayer>(out player))
        {
            _playerTransform = collision.gameObject.GetComponent<Transform>();
            OnFoundPlayer?.Invoke(_playerTransform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IPlayer player;
        if (collision.gameObject.TryGetComponent<IPlayer>(out player))
            OnWatchPlayer?.Invoke(_playerTransform.position);
    }
}

