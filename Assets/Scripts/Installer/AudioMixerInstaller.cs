using UnityEngine;
using Zenject;

public class AudioMixerInstaller : MonoInstaller
{
    [SerializeField] private ClipData _soundData;

    public override void InstallBindings()
    {
        Container.Bind<ClipData>().FromInstance(_soundData).AsSingle();
        Container.Bind<IClipData>().FromMethod(ctx => ctx.Container.Resolve<ClipData>()).AsSingle();

        Container.BindInterfacesTo<AudioMixer>().FromNewComponentOnNewGameObject().AsSingle();
    }
}