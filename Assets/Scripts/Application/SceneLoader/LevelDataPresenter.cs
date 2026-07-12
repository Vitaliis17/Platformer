using R3;
using UnityEngine;
using Zenject;

public class LevelDataPresenter : MonoBehaviour
{
    [Inject] private IHaveLevelLoaderEvent _levelEvent;

    [SerializeField] private LevelData _levelData;

    private void Start()
        => _levelEvent.LevelChanging.Subscribe(levelNumber => _levelData.SetCurrentLevel(levelNumber)).AddTo(this);
}