using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerAnimationPresenter : IInitializable, IDisposable
{
    [Inject(Id = TriggerNames.GroundChecker)] private IHaveTriggerEvent _groundChecker;

    [Inject(Id = TriggerNames.Ladder)] private IHaveTriggerEvent _ladderMap;

    [Inject] private IJumperEvents _jumperEvents;

    [Inject] private IAnimationSwitcher _animationSwitcher;
    [Inject] private IAnimationData _landingData;

    [Inject] private IMovableEvents _events;

    private DisposableBag _bag;

    public void Initialize()
    {
        SubscribeHorizontalMoving();
        SubscribeClimbing();
        SubscribeJumping();
        SubscribeFalling();
        SubscribeLanding();
    }

    public void Dispose()
    {
        _bag.Dispose();

        _animationSwitcher?.Dispose();
    }

    private void SubscribeHorizontalMoving()
    {
        const int OffsetMultiplier = 2;

        SubscribeStartingAnimation(_events.IsHorizontalMoved, AnimationNames.Walking);

        _bag.Add(_events.IsHorizontalMoved
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Walking)));
    }

    private void SubscribeClimbing()
    {
        const int OffsetMultiplier = 2;

        SubscribeStartingAnimation(_events.IsVerticalMoved, AnimationNames.Climbing);

        _bag.Add(_ladderMap.IsTriggered
            .Where(trigger => trigger)
            .Where(_ => _groundChecker.IsTriggered.CurrentValue == false)
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Climbing)));

        _bag.Add(_ladderMap.IsTriggered
            .Where(isTrigger => isTrigger == false)
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Climbing)));

        _bag.Add(_events.IsVerticalMoved
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Where(_ => _groundChecker.IsTriggered.CurrentValue)
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Climbing)));
    }

    private void SubscribeFalling()
    {
        _bag.Add(_groundChecker.IsTriggered
            .Where(_ => _groundChecker.HaveTriggered() == false)
            .Delay(TimeSpan.FromSeconds(1))
            .Where(_ => _groundChecker.HaveTriggered() == false)
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Falling)));

        _bag.Add(_groundChecker.IsTriggered
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Where(_ => _groundChecker.HaveTriggered())
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Falling)));
    }

    private void SubscribeLanding()
    {
        _bag.Add(_groundChecker.IsTriggered
            .Where(_ => _animationSwitcher.CurrentAnimationName == AnimationNames.Falling)
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Where(_ => _groundChecker.HaveTriggered())
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Landing)));

        _bag.Add(_animationSwitcher.Setted
            .Where(animationName => animationName == AnimationNames.Landing)
            .Subscribe(_ => Observable.Timer(TimeSpan.FromSeconds(_landingData.AnimationTime))
                                .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Landing))));
    }

    private void SubscribeJumping()
    {
        _bag.Add(_jumperEvents.IsJumped
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Jumping)));

        _bag.Add(_animationSwitcher.Setted
            .Where(animationName => animationName == AnimationNames.Jumping)
            .Subscribe(_ => Observable.Timer(TimeSpan.FromSeconds(1))
                                .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Jumping))));

        _bag.Add(_groundChecker.IsTriggered
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Where(_ => _groundChecker.HaveTriggered())
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Jumping)));
    }

    private void SubscribeStartingAnimation(Observable<Unit> observable, AnimationNames name)
    {
        _bag.Add(observable
            .Subscribe(_ => _animationSwitcher.SetCurrent(name)));
    }
}