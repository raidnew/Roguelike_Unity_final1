using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SfxSounds { PlayerShot, PlayerJump, DeamonFire, DeamonHit, ALienShot, AlienDied, DeamonDied }

[Serializable]
public class SoundAssociation
{
    [SerializeField] public SfxSound _id;
    [SerializeField] public AudioResource _sound;
}

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ToggleButton _musicButton;
    [SerializeField] private Transform _sfxSoundsContainer;
    [SerializeField] private SfxSound _sfxPrefab;
    [SerializeField] private SoundAssociation[] _soundSources;


    private bool _audioEnabled = true;
    private List<SfxSound> _sfxSources;
    private static AudioController _instance;

    public static void PlaySfxSound(SfxSound sound)
    {
        _instance.PlaySfx(sound);
    }

    private void PlaySfx(SfxSound sound)
    {
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

    private AudioResource GetSound(SfxSound sound)
    {
        foreach (SoundAssociation soundAssocianion in _soundSources)
        {
            if(soundAssocianion._id == sound)
                return soundAssocianion._sound;
        }
        return null;
    }

    private void OnEnable()
    {
        _musicButton.Toggle += OnToggleMusic;
        _instance = this;
    }

    private void OnDisable()
    {
        _musicButton.Toggle -= OnToggleMusic;
    }

    private void OnToggleMusic(int state)
    {
        if(_audioEnabled = (state == 0))
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
