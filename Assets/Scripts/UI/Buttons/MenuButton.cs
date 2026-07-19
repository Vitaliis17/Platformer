using Zenject;
using R3;

public class MenuButton : ButtonSubscriber
{
    [Inject] private IPauseSwitcher _pauseSwitcher;
    [Inject] private IMenuLoader _menuLoader;

    private void Start()
    {
        Observable.Subscribe(_ => _pauseSwitcher.Unpause());
        Observable.Subscribe(_ => _menuLoader.LoadMenu());
    }
}
