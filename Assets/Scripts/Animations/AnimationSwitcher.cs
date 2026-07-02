using UnityEngine;
using Zenject;

public class AnimationSwitcher : IAnimationSwitcher
{
    private readonly AnimationPrioritiesData _priorities;

    private readonly Animator _animator;

    private AnimationNames _lastAnimationName;

    [Inject]
    public AnimationSwitcher(AnimationPrioritiesData priorities, Animator animator)
    {
        _priorities = priorities;
        _animator = animator;

        SetDefault();
    }

    public AnimationNames CurrentAnimationName { get; private set; }

    public void SetCurrent(AnimationNames name)
    {
        if (_priorities.IsMostPriority(name, CurrentAnimationName) == false)
            return;

        ChangeAnimation(name);
    }

    public void TurnOffAnimation(AnimationNames name)
    {
        if (_lastAnimationName == CurrentAnimationName || CurrentAnimationName != name)
            return;

        SetDefault();
    }

    public void SetDefault()
    {
        _lastAnimationName = AnimationNames.Idle;
        _animator.SetBool(AnimationParameterHashes.GetHash(CurrentAnimationName), false);
        
        SetAnimation(AnimationNames.Idle);
    }

    private void ChangeAnimation(AnimationNames name)
    {
        const bool TurnOffValue = false;

        _animator.SetBool(AnimationParameterHashes.GetHash(CurrentAnimationName), TurnOffValue);
        _lastAnimationName = CurrentAnimationName;

        SetAnimation(name);
    }

    private void SetAnimation(AnimationNames name)
    {
        const bool TurnOnValue = true;

        CurrentAnimationName = name;

        _animator.SetBool(AnimationParameterHashes.GetHash(CurrentAnimationName), TurnOnValue);
        _animator.Play(AnimationNameHashes.GetHash(CurrentAnimationName));
    }
}