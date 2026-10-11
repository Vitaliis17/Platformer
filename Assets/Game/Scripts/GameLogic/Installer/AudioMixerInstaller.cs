using UnityEngine;
using Zenject;

public class AudioMixerInstaller : MonoInstaller
{
    [SerializeField] private ClipData _soundData;

    public override void InstallBindings()
    {
        Container.Bind<ClipData>().FromInstance(_soundData).AsSingle();
        Container.Bind<IClipData>().FromMethod(ctx => ctx.Container.Resolve<ClipData>()).AsSingle();

        Container.Bind<AudioMixer>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<IMuteSwitcher>().FromMethod(ctx => ctx.Container.Resolve<AudioMixer>()).AsSingle();
        Container.Bind<IClipSetter>().FromMethod(ctx => ctx.Container.Resolve<AudioMixer>()).AsSingle();
        Container.Bind<IMuter>().FromMethod(ctx => ctx.Container.Resolve<AudioMixer>()).AsSingle();
    }
}