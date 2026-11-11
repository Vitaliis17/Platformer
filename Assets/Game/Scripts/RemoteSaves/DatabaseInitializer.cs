using Zenject;
using System;
using R3;

public class DatabaseInitializer : IInitializable, IDisposable
{
    [Inject] private IReferenceSetter _setter;
    [Inject] private IHaveAuthenticateEvent _event;

    private IDisposable _disposable;

    public void Initialize()
        => _disposable = _event.Authenticated.Take(1).Subscribe(userId => _setter.SetReference(userId));

    public void Dispose()
        => _disposable?.Dispose();
}
