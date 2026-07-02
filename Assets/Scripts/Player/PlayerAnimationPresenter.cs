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

        Subscribe(_events.IsHorizontalMoved, AnimationNames.Walking);

        _events.IsVerticalMoved.Subscribe(_ => _animationSwitcher.SetCurrent(AnimationNames.Climbing)).AddTo(this);

        _events.IsVerticalMoved.Where(_ => _groundChecker.IsTriggered.CurrentValue)
            .Debounce(TimeSpan.FromSeconds(Time.fixedDeltaTime * OffsetMultiplier))
            .Subscribe(_ => _animationSwitcher.TurnOffAnimation(AnimationNames.Climbing)).AddTo(this);

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