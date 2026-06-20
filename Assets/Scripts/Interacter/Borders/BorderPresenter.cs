using UnityEngine;
using Zenject;
using R3;

public class BorderPresenter : MonoBehaviour
{
    [SerializeField] private Transform _gameOverPanel;

    [Inject] private ITrigger _trigger;
    [Inject] private IPauseSwitcher _pauseSwitcher;

    private void Start()
    {
        _trigger.IsTriggered.Subscribe(_ => _gameOverPanel.gameObject.SetActive(true)).AddTo(this);
        _trigger.IsTriggered.Subscribe(_ => _pauseSwitcher.Pause()).AddTo(this);
    }
}
