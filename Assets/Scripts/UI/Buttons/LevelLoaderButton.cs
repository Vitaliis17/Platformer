using R3;
using UnityEngine;
using Zenject;

public class LevelLoaderButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;
    [Inject] private IClipSetter _clipSetter;

    [SerializeField] private int _levelNumber;

    private void Start()
    {
        Observable.Subscribe(_ => _levelLoader.LoadLevel(_levelNumber)).AddTo(this);
        Observable.Subscribe(_ => _clipSetter.SetGameClip()).AddTo(this);
    }
}
