public interface IAnimationSwitcher
{
    void SetCurrent(AnimationNames name);

    void TurnOffAnimation(AnimationNames name);

    void SetDefault();
}