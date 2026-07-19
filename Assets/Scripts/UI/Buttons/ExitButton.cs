using Zenject;
using R3;

public class ExitButton : ButtonSubscriber
{
    [Inject] private IQuiter _quiter;

    private void Start()
        => Observable.Subscribe(_ => _quiter.Quit());
}