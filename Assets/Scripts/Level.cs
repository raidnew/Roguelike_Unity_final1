using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Action LevelFailed;
    public static Action LevelWin;

    [SerializeField] private Astronaut _player;

    private void OnEnable()
    {
        InitEvents();
    }

    private void OnDisable()
    {
        DeinitEvents();
    }

    private void DeinitEvents()
    {
        _player.Die -= OnPlayerDied;
    }

    private void InitEvents()
    {
        _player.Die += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        LevelFailed?.Invoke();
    }
}
