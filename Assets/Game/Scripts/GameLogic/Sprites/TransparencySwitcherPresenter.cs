using R3;
using UnityEngine;
using Zenject;

public class TransparencySwitcherPresenter : MonoBehaviour
{
    [Inject] private IContainer _container;
    [Inject] private ITransparencySwitcher _switcher;

    private void Start()
    {
        _container.Setted.Subscribe(_ => _switcher.Activate()).AddTo(this);
        _container.EmptySetted.Subscribe(_ => _switcher.Deactivate()).AddTo(this);
    }
}