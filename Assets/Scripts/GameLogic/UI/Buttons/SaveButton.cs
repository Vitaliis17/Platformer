using UnityEngine;
using Zenject;
using R3;

public class SaveButton : ButtonSubscriber
{
    [Inject] private ISaver _saver;

    [SerializeField] private int _levelNumber;

    private void Start()
        => Observable.Subscribe(_ => _saver.Save(_levelNumber)).AddTo(this);
}
