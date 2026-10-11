using UnityEngine;
using R3;

public interface IHavePositionEvent
{
    Observable<Vector2> PositionChanged { get; }
}