using Zenject;
using R3;

public class RetryLevelButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;
    [Inject] private ILevelData _data;

    [Inject] private IClipSetter _clipSetter;
    [Inject] private IUnpauser _unpauser;

    private void Start()
    {
        Observable.Subscribe(_ => _levelLoader.LoadLevel(_data.CurrentLevel)).AddTo(this);
        Observable.Subscribe(_ => _clipSetter.SetGameClip()).AddTo(this);
        Observable.Subscribe(_ => _unpauser.Unpause()).AddTo(this);
    }
}
