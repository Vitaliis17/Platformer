using R3;
using System;
using Zenject;

public class TransparencySwitcherPresenter : IInitializable, IDisposable
{
    [Inject] private IContainer _container;
    [Inject] private ITransparencySwitcher _switcher;

    private DisposableBag _bag;

    public void Initialize()
    {
        _bag.Add(_container.Setted.Subscribe(_ => _switcher.Activate()));
        _bag.Add(_container.EmptySetted.Subscribe(_ => _switcher.Deactivate()));
    }

    public void Dispose()
        => _bag.Dispose();
}