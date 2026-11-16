using UnityEngine;

[CreateAssetMenu(fileName = nameof(ButtonData), menuName = nameof(ButtonData))]
public class ButtonData : ScriptableObject, IButtonData
{
    [SerializeField] private Vector2 _idlePostion;
    [SerializeField] private Vector2 _pressedPosition;

    [SerializeField, Min(0)] private float _animationTime;

    public Vector2 IdlePosition => _idlePostion;
    public Vector2 PressedPosition => _pressedPosition;

    public float AnimationTime => _animationTime;
}