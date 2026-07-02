using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerAnimationPresenter : MonoBehaviour
{
    [SerializeField] private Trigger _groundChecker;

    [Inject] private IAnimationSwitcher _animationSwitcher;
    [Inject] private IMovableEvents _events;

    private void Start()
    {
        const int OffsetMultiplier = 2;

        SubscribeStartingAnimation(_events.IsHorizontalMoved, AnimationNames.Walking);

        _events.IsHorizontalMoved
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Walking))
            .AddTo(this);

        SubscribeStartingAnimation(_events.IsVerticalMoved, AnimationNames.Climbing);

        _events.IsVerticalMoved
            .Where(_ => _groundChecker.IsTriggered.CurrentValue)
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Climbing))
            .AddTo(this);

        SubscribeStartingAnimation(_groundChecker.IsTriggered.AsUnitObservable(), AnimationNames.Jumping);

        _groundChecker.IsTriggered
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Where(_ => _animationSwitcher.CurrentAnimationName == AnimationNames.Jumping)
            .Where(isTrigger => isTrigger)
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Jumping))
            .AddTo(this);
    }

    private void SubscribeStartingAnimation(Observable<Unit> observable, AnimationNames name)
    {
        observable
            .Subscribe(_ => _animationSwitcher.SetCurrent(name))
            .AddTo(this);
    }
}