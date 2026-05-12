using UnityEngine;
using R3;

public interface IMovementReader
{
    public Observable<Vector2> DirectionChanged { get; }
}
