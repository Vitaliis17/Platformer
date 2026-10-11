using Zenject;
using R3;

public class NextLevelLoaderButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;
    [Inject] private ILevelData _data;
    
    [Inject] private IClipSetter _clipSetter;
    [Inject] private IUnpauser _unpauser;

    private void Start()
    {
        const int LevelStep = 1;

        int nextLevel = _data.CurrentLevel + LevelStep;

        Observable.Subscribe(_ => _levelLoader.LoadLevel(nextLevel)).AddTo(this);
        Observable.Subscribe(_ => _clipSetter.SetGameClip()).AddTo(this);
        Observable.Subscribe(_ => _unpauser.Unpause()).AddTo(this);
    }
}