using Zenject;
using R3;

public class ContinueButton : ButtonSubscriber
{
    [Inject] private IUnpauser _unpauser;

    private void Start()
        => Observable.Subscribe(_ => _unpauser.Unpause());
}
