using UnityEngine;

public class Container : MonoBehaviour, IContainerSetter
{
    private IInteractable _current;

    public void Set(IInteractable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;

        ((MonoBehaviour)_current).transform.position = transform.position;
    }
}
