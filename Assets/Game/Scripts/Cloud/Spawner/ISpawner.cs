using UnityEngine;

public interface ISpawner<T> where T : Component
{
    T GetElement();

    void ReleaseElement(T element);
}