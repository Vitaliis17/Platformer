using R3;
using System;

public interface IAnimationSwitcher : IDisposable
{
    Observable<AnimationNames> Setted { get; }
    
    AnimationNames CurrentAnimationName { get; }

    void SetCurrent(AnimationNames name);

    void TurnOffAnimation(AnimationNames name);

    void SetDefault();
}