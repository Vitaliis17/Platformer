using UnityEngine;
using Zenject;
using R3;

public class LevelLoaderButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;

    [SerializeField, Min(1)] private int _levelNumber;

    private void Start()
        => Observable.Subscribe(_ => _levelLoader.LoadLevel(_levelNumber)).AddTo(this);
}
