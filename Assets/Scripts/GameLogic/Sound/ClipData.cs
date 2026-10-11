using UnityEngine;

[CreateAssetMenu(fileName = nameof(ClipData), menuName = nameof(ClipData))]
public class ClipData : ScriptableObject, IClipData
{
    [SerializeField] private AudioClip _menuClip;
    [SerializeField] private AudioClip _gameClip;

    public AudioClip MenuClip => _menuClip;
    public AudioClip GameClip => _gameClip;
}