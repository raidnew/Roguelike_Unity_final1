using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Action LevelFailed;
    public static Action LevelWin;

    [SerializeField] private Astronaut _player;

    private static int _countStarts = 0;
    public static int CountStarts {  get { return _countStarts; } }

    private void OnEnable()
    {
        InitEvents();
    }

    private void OnDisable()
    {
        DeinitEvents();
    }

    private void Start()
    {
        StartLevel();
    }

    private void DeinitEvents()
    {
        _player.Die -= OnPlayerDied;
    }

    private void StartLevel()
    {
        _countStarts++;
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
