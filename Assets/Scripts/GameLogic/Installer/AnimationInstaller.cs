using UnityEngine;
using Zenject;

public class AnimationInstaller : MonoInstaller
{
    [SerializeField] private AnimationPrioritiesData _animationPrioritiesData;

    [SerializeField] private Animator _playerAnimator;

    public override void InstallBindings()
    {
        BindAnimation();
        BindFlipper();
    }

    private void BindAnimation()
    {
        Container.Bind<AnimationPrioritiesData>().FromInstance(_animationPrioritiesData).AsSingle();
        Container.Bind<IAnimationPrioritiesData>().FromMethod(ctx => ctx.Container.Resolve<AnimationPrioritiesData>()).AsSingle();

        Container.Bind<IAnimationExpecter>().To<AnimationExpecter>().AsTransient();

        Container.Bind<Animator>().FromInstance(_playerAnimator).AsSingle();

        Container.Bind<IAnimationSwitcher>().To<AnimationSwitcher>().AsTransient();
    }

    private void BindFlipper()
        => Container.Bind<IFlipper>().To<Flipper>().AsSingle();
}