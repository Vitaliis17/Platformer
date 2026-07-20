using UnityEngine;
using Zenject;
using R3;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] private Transform _gameOverPanel;

    [Inject(Id = TriggerNames.Border)] private IHaveTriggerEvent _trigger;
    [Inject] private IPauser _pauser;

    private void Start()
    {
        Observable<bool> observable = _trigger.IsTriggered.Where(isTrigger => isTrigger);

        observable.Subscribe(isTrigger => _gameOverPanel.gameObject.SetActive(isTrigger)).AddTo(this);
        observable.Subscribe(_ => _pauser.Pause()).AddTo(this);
    }
}