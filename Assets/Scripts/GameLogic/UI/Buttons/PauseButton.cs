using Zenject;
using R3;

public class PauseButton : ButtonSubscriber
{
    [Inject] private IPauser _pauser;

    private void Start()
        => Observable.Subscribe(_ => _pauser.Pause()).AddTo(this);
}
