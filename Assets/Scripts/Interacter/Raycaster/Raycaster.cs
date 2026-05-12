using UnityEngine;
using UnityEngine.InputSystem;

public class Raycaster : MonoBehaviour, IRaycaster<IInteractable>
{
    public IInteractable Raycast()
    {
        Vector2 position = Pointer.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider == null || hit.collider.TryGetComponent(out IInteractable interactable) == false)
            return null;

        return interactable;
    }
}
