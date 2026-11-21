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

    [Inject] private IEmptyCheckerExceptItems _containerChecker;

    private DisposableBag _bag;

    private bool _isGrounded;

    public void Initialize()
    {
        _bag.Add(_groundChecker.IsTriggered.Subscribe(_ => _isGrounded = _groundChecker.HaveTriggered()));

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
            .Where(_ => _isGrounded == false)
            .Where(_ => _containerChecker.HaveNoOrExceptItems())
            .Select(_ => Observable.Timer(TimeSpan.FromSeconds(0.5))
                            .TakeWhile(_ => _isGrounded == false))
            .Switch()
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Falling)));

        _bag.Add(_groundChecker.IsTriggered
            .Where(_ => _isGrounded == false)
            .Where(_ => _containerChecker.HaveNoOrExceptItems() == false)
            .Delay(TimeSpan.FromSeconds(0.1))
            .Where(_ => _isGrounded == false)
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Falling)));

        _bag.Add(_groundChecker.IsTriggered
            .Where(_ => _isGrounded)
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Falling)));
    }

    private void SubscribeLanding()
    {
        _bag.Add(_groundChecker.IsTriggered
            .Where(_ => _animationSwitcher.CurrentAnimationName == AnimationNames.Falling)
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Where(_ => _isGrounded)
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Landing)));

        _bag.Add(_animationSwitcher.Setted
            .Where(animationName => animationName == AnimationNames.Landing)
            .Select(_ => Observable.Timer(TimeSpan.FromSeconds(_landingData.AnimationTime)))
            .Switch()
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Landing)));
    }

    private void SubscribeJumping()
    {
        _bag.Add(_jumperEvents.IsJumped
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Jumping)));

        _bag.Add(_animationSwitcher.Setted
            .Where(animationName => animationName == AnimationNames.Jumping)
            .Select(_ => Observable.Timer(TimeSpan.FromSeconds(1.3))
                                   .TakeWhile(_ => _isGrounded == false))
            .Switch()
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Jumping)));

        _bag.Add(_groundChecker.IsTriggered
            .Delay(TimeSpan.FromSeconds(Time.deltaTime))
            .Where(_ => _isGrounded)
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Jumping)));
    }

    private void SubscribeStartingAnimation(Observable<Unit> observable, AnimationNames name)
    {
        _bag.Add(observable
            .Subscribe(_ => _animationSwitcher.SetCurrent(name)));
    }
}