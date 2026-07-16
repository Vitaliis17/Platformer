using R3;
using UnityEngine;
using Zenject;

public class LevelLoaderButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;

    [SerializeField] private int _levelNumber;

    private void Start()
    {
        Observable
            .Subscribe(_ => _levelLoader.LoadLevel(_levelNumber))
            .AddTo(this);
    }
}
