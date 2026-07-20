using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class AudioMixer : MonoBehaviour, IClipSetter, IMuter
{
    [Inject] private IClipData _clipData;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;

        SetMenuClip();
    }

    public void SetMenuClip()
        => SetClip(_clipData.MenuClip);

    public void SetGameClip()
        => SetClip(_clipData.GameClip);

    public void Mute()
        => _audioSource.mute = true;

    public void Unmute()
        => _audioSource.mute = false;

    public bool IsMute()
        => _audioSource.mute;

    private void SetClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}