using UnityEngine;

public interface ITransferator<T>
{
    void Transfer(Vector2 delta, T transferObject);

    Vector2 GetDeltaPosition(Vector2 delta);
}