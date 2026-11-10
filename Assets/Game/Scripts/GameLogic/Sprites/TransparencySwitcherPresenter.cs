using R3;
using System;
using Zenject;

public class TransparencySwitcherPresenter : IInitializable, IDisposable
{
    [Inject] private IContainer _container;

    [Inject(Id = TransparencySwitcherId.Container)] private ITransparencySwitcher _containerSwitcher;
    [Inject(Id = TransparencySwitcherId.Border)] private ITransparencySwitcher _borderSwitcher;

    private DisposableBag _bag;

    public void Initialize()
    {
        SubscribeSwitcher(_containerSwitcher);
        SubscribeSwitcher(_borderSwitcher);
    }

    public void Dispose()
        => _bag.Dispose();

    private void SubscribeSwitcher(ITransparencySwitcher switcher)
    {
        _bag.Add(_container.Setted.Subscribe(_ => switcher.Activate()));
        _bag.Add(_container.EmptySetted.Subscribe(_ => switcher.Deactivate()));
    }
}