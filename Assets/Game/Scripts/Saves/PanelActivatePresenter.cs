using System;
using Zenject;
using R3;

public class PanelActivatePresenter : IInitializable, IDisposable
{
    [Inject] private IActivater _panelActivater;
    [Inject] private IHaveSaveCompareEvent _compapeEvent;

    private IDisposable _disposable;

    public void Initialize()
        => _disposable = _compapeEvent.HasFirebaseMoreSaves.Where(hasMore => hasMore).Subscribe(_ => _panelActivater.Activate());

    public void Dispose()
        => _disposable.Dispose();
}