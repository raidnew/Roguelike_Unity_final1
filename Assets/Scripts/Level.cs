using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Action LevelFailed;
    public static Action LevelWin;
    public static Action LevelQuit;
    public static Action LevelEnd;

    [SerializeField] private Astronaut _player;
    [SerializeField] private AudioController _sounds;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private LevelPauseScreen _pauseScreen;

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

    private void StartLevel()
    {
    }

    private void InitEvents()
    {
        _player.Die += OnPlayerDied;
        _player.BeginBullet += OnBeginBullet;
        _pauseButton.Click += OnPauseGame;
        FinishArea.Complete += OnFinishReached;
        LevelPauseScreen.MainMenu += OnLevelQuit;
    }

    private void DeinitEvents()
    {
        _player.Die -= OnPlayerDied;
        _player.BeginBullet -= OnBeginBullet;
        _pauseButton.Click -= OnPauseGame;
        FinishArea.Complete -= OnFinishReached;
        LevelPauseScreen.MainMenu -= OnLevelQuit;
    }

    private void OnPauseGame()
    {
        _pauseScreen.gameObject.SetActive(true);
    }

    private void OnPlayerDied()
    {
        _countStarts++;
        LevelEnd?.Invoke();
        LevelFailed?.Invoke();
    }

    private void OnLevelQuit()
    {
        LevelEnd?.Invoke();
        LevelQuit?.Invoke();
    }

    private void OnBeginBullet(Bullet bullet)
    {
        bullet.transform.parent = transform;
    }

    private void OnFinishReached()
    {
        LevelEnd?.Invoke();
        LevelWin?.Invoke();
    }
}
