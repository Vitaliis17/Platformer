using R3;
using System;

public class Container : IContainer, IDisposable
{
    private readonly Subject<Unit> _setted = new();
    private readonly Subject<Unit> _emptySetted = new();

    private ITransferable _current;

    public Observable<Unit> Setted => _setted;
    public Observable<Unit> EmptySetted => _emptySetted;

    public void Set(ITransferable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;

        _setted.OnNext(Unit.Default);
    }

    public void SetEmpty()
    {
        _current = null;

        _emptySetted.OnNext(Unit.Default);
    }

    public ITransferable Get()
        => _current;

    public bool IsEmpty()
        => _current == null;

    public void Dispose()
    {
        _setted?.Dispose();
        _emptySetted?.Dispose();
    }
}
