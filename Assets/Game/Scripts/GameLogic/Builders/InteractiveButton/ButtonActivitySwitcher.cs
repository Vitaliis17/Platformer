using UnityEngine;
using Zenject;
using DG.Tweening;
using R3;
using System;

internal class ButtonActivitySwitcher : IHaveActivityWithEvent, IDisposable
{
    private IButtonData _data;
    
    private Transform _button;

    private Subject<Unit> _activated;
    private Subject<Unit> _deactivated;

    public Observable<Unit> Activated => _activated;
    public Observable<Unit> Deactivated => _deactivated;

    [Inject]
    public ButtonActivitySwitcher(Transform button, IButtonData data)
    {
        _data = data;
        _button = button;

        _activated = new();
        _deactivated = new();
    }

    public void Dispose()
    {
        _activated?.Dispose();
        _deactivated?.Dispose();
    }

    public void Activate()
        => _button.DOLocalMove(_data.PressedPosition, _data.AnimationTime).OnComplete(() => _activated.OnNext(Unit.Default));

    public void Deactivate()
        => _button.DOLocalMove(_data.IdlePosition, _data.AnimationTime).OnComplete(() => _deactivated.OnNext(Unit.Default));
}