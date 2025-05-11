using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
//[RequireComponent(typeof(Collider2D))]
public class FlyBehaviour : MonoBehaviour
{
    private IEnemy _enemy;
    [SerializeField] private int _currentDirection = 1;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void ChooseMyAction()
    {
        _enemy.Move(_currentDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IGround ground;
        if(collision.TryGetComponent<IGround>(out ground))
            _currentDirection = -_currentDirection;

    }

    private void FixedUpdate()
    {
        ChooseMyAction();
    }

}
