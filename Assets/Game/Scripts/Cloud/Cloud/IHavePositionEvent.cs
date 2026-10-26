using UnityEngine;
using R3;

internal interface IHavePositionEvent
{
    Observable<Vector2> PositionChanged { get; }
}