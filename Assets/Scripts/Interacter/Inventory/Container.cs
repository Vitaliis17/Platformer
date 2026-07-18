public class Container : IContainer
{
    private ITransferable _current;

    public void Set(ITransferable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;
    }

    public void SetEmpty()
        => _current = null;

    public ITransferable Get()
        => _current;

    public bool IsEmpty()
        => _current == null;
}
