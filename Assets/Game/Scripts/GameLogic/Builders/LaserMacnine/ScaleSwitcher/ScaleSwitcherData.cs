using UnityEngine;

[CreateAssetMenu(fileName = nameof(ScaleSwitcherData), menuName = nameof(ScaleSwitcherData))]
public class ScaleSwitcherData : ScriptableObject, IScaleSwitcherData
{
    [SerializeField] private float _animationTime;

    public float AnimationTime => _animationTime;
}