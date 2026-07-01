using UnityEngine;
using Zenject;

public class AnimationSwitcher : IAnimationSwitcher
{
    private readonly AnimationPrioritiesData _priorities;

    private readonly Animator _animator;

    private AnimationNames _lastAnimationName;
    private AnimationNames _currentAnimationName;

    [Inject]
    public AnimationSwitcher(AnimationPrioritiesData priorities, Animator animator)
    {
        _priorities = priorities;
        _animator = animator;

        SetDefault();
    }

    public void SetCurrent(AnimationNames name)
    {
        if (_priorities.IsMostPriority(name, _currentAnimationName) == false)
            return;

        ChangeAnimation(name);
    }

    public void TurnOffAnimation(AnimationNames name)
    {
        if (_lastAnimationName == _currentAnimationName || _currentAnimationName != name)
            return;

        SetDefault();
    }

    public void SetDefault()
    {
        _lastAnimationName = AnimationNames.Idle;
        _animator.SetBool(AnimationParameterHashes.GetHash(_currentAnimationName), false);
        
        SetAnimation(AnimationNames.Idle);
    }

    private void ChangeAnimation(AnimationNames name)
    {
        const bool TurnOffValue = false;

        _animator.SetBool(AnimationParameterHashes.GetHash(_currentAnimationName), TurnOffValue);
        _lastAnimationName = _currentAnimationName;

        SetAnimation(name);
    }

    private void SetAnimation(AnimationNames name)
    {
        const bool TurnOnValue = true;

        _currentAnimationName = name;

        _animator.SetBool(AnimationParameterHashes.GetHash(_currentAnimationName), TurnOnValue);
        _animator.Play(AnimationNameHashes.GetHash(_currentAnimationName));
    }
}