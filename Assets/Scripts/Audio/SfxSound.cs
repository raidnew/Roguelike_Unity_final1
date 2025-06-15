using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioSource))]
public class SfxSound : MonoBehaviour
{
    public Action<SfxSound> Finish;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource> ();
    }

    private void Update()
    {
        if (_audioSource != null && !_audioSource.isPlaying)
            Finish?.Invoke(this);
    }

    public void PlaySound(AudioResource sound)
    {
        _audioSource.resource = sound;
        _audioSource.Play();
        
    }
}
