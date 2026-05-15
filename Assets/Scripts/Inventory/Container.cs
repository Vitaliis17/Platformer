using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
    private IInteractable _current;

    public void SetItem(IInteractable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;

        ((MonoBehaviour)_current).transform.position = transform.position;
    }
}
