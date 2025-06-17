using System;
using UnityEngine;

public class LevelPauseScreen : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    public static Action MainMenu;
    public static Action Resume;

    private void OnEnable()
    {
        InitEvents();
    }

    private void OnDisable()
    {
        DeinitEvents();
    }

    private void InitEvents()
    {
        _resumeButton.Click += OnResumeClick;
        _mainMenuButton.Click += OnMainMenuClick;
    }

    private void DeinitEvents()
    {
        _resumeButton.Click -= OnResumeClick;
        _mainMenuButton.Click -= OnMainMenuClick;
    }

    private void OnResumeClick()
    {
        Resume?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnMainMenuClick()
    {
        MainMenu?.Invoke();
        gameObject.SetActive(false);
    }
}
