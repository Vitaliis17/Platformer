using UnityEngine;
using R3;

public interface IHoldReader
{
    public Observable<Vector2> HoldChanged { get; }
}