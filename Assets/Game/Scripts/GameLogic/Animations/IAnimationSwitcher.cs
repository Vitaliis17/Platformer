using R3;
using System;

public interface IAnimationSwitcher : ICurrentAnimationNameContainer, IDisposable
{
    Observable<AnimationNames> Setted { get; }
    
    void SetCurrent(AnimationNames name);

    void TurnOffAnimation(AnimationNames name);

    void SetDefault();
}