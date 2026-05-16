public class Container : IContainer
{
    private IInteractable _current;

    public void Set(IInteractable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;
    }

    public void SetEmpty()
        => _current = null;

    public IInteractable Get()
        => _current;

    public bool IsEmpty()
        => _current == null;
}
