using UnityEngine;
using Zenject;
using R3;

public class RetryLevelButton : ButtonSubscriber
{
    [Inject] private ILevelLoader _levelLoader;

    [SerializeField] private LevelData _data;

    private void Start()
        => Observable.Subscribe(_ => _levelLoader.LoadLevel(_data.CurrentLevel)).AddTo(this);
}
