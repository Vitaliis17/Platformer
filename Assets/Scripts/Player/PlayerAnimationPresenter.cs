using System;
using UnityEngine;
using Zenject;
using R3;

public class PlayerAnimationPresenter : MonoBehaviour
{
    [Inject] private IAnimationSwitcher _animationSwitcher;
    [Inject] private IMovableEvents _events;

    private void Start()
    {
        Subscribe(_events.IsHorizontalMoved, AnimationNames.Walking);
        Subscribe(_events.IsVerticalMoved, AnimationNames.Climbing);
        Subscribe(_events.IsJumped, AnimationNames.Jumping);
    }

    private void Subscribe(Observable<Unit> observable, AnimationNames name)
    {
        const int OffsetMultiplier = 2;

        observable.Subscribe(_ => _animationSwitcher.SetCurrent(name)).AddTo(this);

        observable.Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(name)).AddTo(this);
    }
}