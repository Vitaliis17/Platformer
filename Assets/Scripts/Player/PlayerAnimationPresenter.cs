using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerAnimationPresenter : MonoBehaviour
{
    [Inject(Id = TriggerNames.GroundChecker)] private Trigger _groundChecker;

    [Inject(Id = TriggerNames.Ladder)] private IHaveTriggerEvent _ladderMap;

    [Inject] private IAnimationSwitcher _animationSwitcher;
    [Inject] private IMovableEvents _events;

    private void Start()
    {
        SubscribeHorizontalMoving();
        SubscribeClimbing();
        SubscribeJumping();
    }

    private void SubscribeHorizontalMoving()
    {
        const int OffsetMultiplier = 2;

        SubscribeStartingAnimation(_events.IsHorizontalMoved, AnimationNames.Walking);

        _events.IsHorizontalMoved
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Walking))
            .AddTo(this);
    }

    private void SubscribeClimbing()
    {
        const int OffsetMultiplier = 2;

        SubscribeStartingAnimation(_events.IsVerticalMoved, AnimationNames.Climbing);

        _ladderMap.IsTriggered
            .Where(isTrigger => isTrigger == false)
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Climbing))
            .AddTo(this);

        _events.IsVerticalMoved
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Where(_ => _groundChecker.IsTriggered.CurrentValue)
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Climbing))
            .AddTo(this);
    }

    private void SubscribeJumping()
    {
        _groundChecker.IsTriggered
            .Where(_ => _groundChecker.HaveTriggered() == false)
            .Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Jumping))
            .AddTo(this);

        _groundChecker.IsTriggered
            .Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime))
            .Where(_ => _groundChecker.HaveTriggered())
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