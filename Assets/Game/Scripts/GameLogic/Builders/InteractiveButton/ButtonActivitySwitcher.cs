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

    public Observable<Unit> Activated => _activated;

    [Inject]
    public ButtonActivitySwitcher(IButtonData data, Transform button)
    {
        _data = data;
        _button = button;

        _activated = new();
    }

    public void Dispose()
        => _activated?.Dispose();

    public void Activate()
        => _button.DOLocalMove(_data.PressedPosition, _data.AnimationTime).OnComplete(() => _activated.OnNext(Unit.Default));

    public void Deactivate()
        => _button.DOLocalMove(_data.IdlePosition, _data.AnimationTime);
}