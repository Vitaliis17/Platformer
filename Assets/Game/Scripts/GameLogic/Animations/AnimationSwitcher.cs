using System;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using R3;

public class AnimationSwitcher : IAnimationSwitcher
{
    private readonly IAnimationPrioritiesData _priorities;
    private readonly IAnimationExpecter _expecter;

    private readonly Animator _animator;

    private Subject<AnimationNames> _setted;

    public Observable<AnimationNames> Setted => _setted;

    [Inject]
    public AnimationSwitcher(IAnimationPrioritiesData priorities, IAnimationExpecter expecter, Animator animator)
    {
        _priorities = priorities;
        _animator = animator;

        _expecter = expecter;

        _setted = new();

        SetDefault();
    }

    public AnimationNames CurrentAnimationName { get; private set; }

    public void Dispose()
        => _setted?.Dispose();

    public void SetCurrent(AnimationNames name)
    {
        if (_priorities.IsMostPriority(name, CurrentAnimationName) == false)
        {
            if (CurrentAnimationName != name)
            {
                _expecter.Add(name);
            }

            return;
        }

        ChangeAnimation(name);
    }

    public void TurnOffAnimation(AnimationNames name)
    {
        _expecter.Remove(name);

        if (CurrentAnimationName != name)
            return;

        SetDefault();
    }

    public void SetDefault()
    {
        _animator.SetBool(AnimationParameterHashes.GetHash(CurrentAnimationName), false);

        List<AnimationNames> names = _expecter.GetNames();
        AnimationNames mostPriorityName = _priorities.IsMostPriority(names);

        _expecter.Remove(mostPriorityName);

        SetAnimation(mostPriorityName);
    }

    private void ChangeAnimation(AnimationNames name)
    {
        const bool TurnOffValue = false;
        
        _animator.SetBool(AnimationParameterHashes.GetHash(CurrentAnimationName), TurnOffValue);

        SetAnimation(name);
    }

    private void SetAnimation(AnimationNames name)
    {
        const bool TurnOnValue = true;

        CurrentAnimationName = name;
        
        _animator.SetBool(AnimationParameterHashes.GetHash(CurrentAnimationName), TurnOnValue);
        _animator.Play(AnimationNameHashes.GetHash(CurrentAnimationName));

        _setted.OnNext(CurrentAnimationName);
    }
}