using R3;
using Zenject;
using System;
using UnityEngine;

public class RegisterPresenter : IInitializable, IDisposable
{
    [Inject(Id = IdNames.Register)] private IHavePressedEvent _event;

    [Inject] private IRegister _register;

    private IDisposable _disposable;

    public void Initialize()
    {
        _disposable = _event.Pressed.Subscribe(info => _register.Register(info.Item1, info.Item2));
    }

    public void Dispose()
        => _disposable?.Dispose();
}