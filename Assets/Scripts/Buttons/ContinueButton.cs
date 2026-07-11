using Zenject;
using R3;

public class ContinueButton : ButtonSubscriber
{
    [Inject] private IPauseSwitcher _pauseSwitcher;

    private void Start()
        => Observable.Subscribe(_ => _pauseSwitcher.Unpause());
}
