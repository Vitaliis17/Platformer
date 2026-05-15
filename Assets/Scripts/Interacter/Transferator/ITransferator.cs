using UnityEngine;

public interface ITransferator<T>
{
    void Transfer(Vector2 delta, T transferObject);
}