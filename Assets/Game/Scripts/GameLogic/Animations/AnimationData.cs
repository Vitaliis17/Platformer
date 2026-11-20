using UnityEngine;

[CreateAssetMenu(fileName = nameof(AnimationData), menuName = nameof(AnimationData))]
public class AnimationData : ScriptableObject, IAnimationData
{
    [SerializeField] private AnimationClip _clip;

    public float AnimationTime { get; private set; }

    private void OnEnable()
        => AnimationTime = _clip.length;
}