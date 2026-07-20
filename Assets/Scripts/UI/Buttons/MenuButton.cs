using Zenject;
using R3;

public class MenuButton : ButtonSubscriber
{
    [Inject] private IMenuLoader _menuLoader;
    [Inject] private IClipSetter _clipSetter;
    [Inject] private IUnpauser _unpauser;

    private void Start()
    {
        Observable.Subscribe(_ => _menuLoader.LoadMenu());
        Observable.Subscribe(_ => _clipSetter.SetMenuClip());
        Observable.Subscribe(_ => _unpauser.Unpause());
    }
}
