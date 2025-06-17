using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class SoundAssociation
{
    [SerializeField] public SfxId _id;
    [SerializeField] public AudioResource _sound;
}

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ToggleButton _musicButton;
    [SerializeField] private ToggleButton _sfxButton;
    [SerializeField] private Transform _sfxSoundsContainer;
    [SerializeField] private SfxSound _sfxPrefab;
    [SerializeField] private SoundAssociation[] _soundSources;

    public bool AudioEnabled
    {
        get; private set;
    }

    public bool SfxEnabled
    {
        get; private set;
    }


    private List<SfxSound> _sfxSources = new List<SfxSound>();
    private static AudioController _instance;

    public static void PlaySfxSound(SfxId sound)
    {
        _instance.PlaySfx(sound);
    }

    private void PlaySfx(SfxId sound)
    {
        if (!SfxEnabled) return;
        AudioResource resource = GetSound(sound);
        if(resource != null){
            SfxSound newSound = Instantiate(_sfxPrefab);
            newSound.transform.parent = _sfxSoundsContainer;
            newSound.PlaySound(resource);
            newSound.Finish += OnSoundFinish;
            _sfxSources.Add(newSound);
        }
    }

    private void OnSoundFinish(SfxSound sfxSound)
    {
        sfxSound.Finish -= OnSoundFinish;
        _sfxSources.Remove(sfxSound);
        Destroy(sfxSound.gameObject);
    }

    private AudioResource GetSound(SfxId sound)
    {
        foreach (SoundAssociation soundAssocianion in _soundSources)
        {
            if(soundAssocianion._id == sound)
                return soundAssocianion._sound;
        }
        return null;
    }

    private void Awake()
    {
        _musicButton.Toggle += OnToggleMusic;
        _sfxButton.Toggle += OnToggleSfx;
        _instance = this;
        OnToggleMusic(_musicButton.CurrentState);
        OnToggleSfx(_sfxButton.CurrentState);
    }

    private void OnDisable()
    {
        _musicButton.Toggle -= OnToggleMusic;
        _sfxButton.Toggle -= OnToggleSfx;
    }

    private void OnToggleSfx(int state)
    {
        SfxEnabled = (state == 0);
    }

    private void OnToggleMusic(int state)
    {
        AudioEnabled = (state == 0);
        if (AudioEnabled)
            _audioSource.Play();
        else
            _audioSource.Stop();
    }
}
