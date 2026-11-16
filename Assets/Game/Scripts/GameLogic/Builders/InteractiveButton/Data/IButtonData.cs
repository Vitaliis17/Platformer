using UnityEngine;

public interface IButtonData
{
    Vector2 IdlePosition { get; }
    Vector2 PressedPosition { get; }

    float AnimationTime { get; }
}