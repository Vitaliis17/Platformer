public class Container : IContainer
{
    private IHavePosition _current;

    public void Set(IHavePosition interactable)
    {
        if (_current != null)
            return;

        _current = interactable;
    }

    public void SetEmpty()
        => _current = null;

    public IHavePosition Get()
        => _current;

    public bool IsEmpty()
        => _current == null;
}
