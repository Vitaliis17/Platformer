using UnityEngine;
using Zenject;
using R3;

public class NextLevelLoaderButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;

    [SerializeField] private LevelData _data;

    private void Start()
    {
        const int LevelStep = 1;

        int nextLevel = _data.CurrentLevel + LevelStep;
        Observable.Subscribe(_ => _levelLoader.LoadLevel(nextLevel)).AddTo(this);
    }
}