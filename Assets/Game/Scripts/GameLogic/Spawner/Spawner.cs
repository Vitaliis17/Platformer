using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : ISpawner<T> where T : Component
{
    private readonly Factory<T> _factory;

    private readonly Transform _container;

    private readonly ObjectPool<T> _pool;

    public Spawner(Factory<T> factory, Transform container)
    {
        _factory = factory;
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
    {
        T component = _factory.Create();
        component.transform.SetParent(_container);

        return component;
    }

    private void DestroyElement(T element)
        => Object.Destroy(element);
}