using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : ISpawner<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _container;

    private readonly ObjectPool<T> _pool;

    public Spawner(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;

        _pool = new(Create, Get, Release, DestroyElement);
    }

    public virtual T GetElement()
        => _pool.Get();

    public void ReleaseElement(T element)
        => _pool.Release(element);

    private void Get(T element)
        => element.gameObject.SetActive(true);

    private void Release(T element)
        => element?.gameObject.SetActive(false);

    private T Create()
        => Object.Instantiate(_prefab, _container);

    private void DestroyElement(T element)
        => Object.Destroy(element);
}