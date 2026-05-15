using UnityEngine;

public class InventoryContainer : MonoBehaviour, IContainerSetter<IInteractable>, IContainerReceiver<IInteractable>
{
    private IInteractable _current;

    public void Set(IInteractable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;

        if (_current is MonoBehaviour moveableObject)
            moveableObject.transform.position = transform.position;
    }

    public IInteractable Get()
    {
        IInteractable interactable = _current;
        _current = null;

        return interactable;
    }
}