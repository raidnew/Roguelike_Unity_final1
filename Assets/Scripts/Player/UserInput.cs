using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Astronaut _interactiveObject;

    void Update()
    {

        float horizontal = Input.GetAxis(Constants.horizontal);

        if (horizontal != 0)
            _interactiveObject.Move(horizontal);

        if (Input.GetButtonDown(Constants.jump))
            _interactiveObject.Jump();

        if (Input.GetButtonDown(Constants.fire1))
            _interactiveObject.Attack();

        if (Input.GetButtonDown(Constants.fire2))
            _interactiveObject.Shoot();

    }
}
