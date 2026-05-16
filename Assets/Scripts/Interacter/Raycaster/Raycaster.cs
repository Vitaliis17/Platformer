using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Raycaster<T> : IRaycaster<T> where T : class
{
    private readonly LayerMask _layerMask;

    [Inject]
    public Raycaster(LayerMask layerMask)
        => _layerMask = layerMask;

    public T Raycast()
    {
        Vector2 position = Pointer.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, (int)_layerMask);

        if (hit.collider == null || hit.collider.TryGetComponent(out T component) == false)
            return null;

        return component;
    }
}