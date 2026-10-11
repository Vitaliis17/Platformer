public interface IAnimationSwitcher
{
    AnimationNames CurrentAnimationName { get; }

    void SetCurrent(AnimationNames name);

    void TurnOffAnimation(AnimationNames name);

    void SetDefault();
}