using UnityEngine;
using R3;

public interface IHoldReader
{
    public ReadOnlyReactiveProperty<Vector2> HoldChanged { get; }
}