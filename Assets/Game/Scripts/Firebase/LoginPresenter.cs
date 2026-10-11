using R3;
using Zenject;
using System;

public class LoginPresenter : IInitializable, IDisposable
{
    [Inject(Id = IdNames.Login)] private IHavePressedEvent _event;

    [Inject] private ICanLogin _login;

    private IDisposable _disposable;

    public void Initialize()
        => _disposable = _event.Pressed.Subscribe(info => _login.Login(info.Item1, info.Item2));

    public void Dispose()
        => _disposable?.Dispose();
}