using UnityEngine;
using R3;
using Zenject;

public class ActivitySwitcher : MonoBehaviour
{
    [SerializeField] private Transform _deactivated;
    [SerializeField] private Transform _activated;

    [Inject] private IHaveLoginEvent _loginEvent;

    private void Start()
    {
        _loginEvent.IsLogin.Subscribe(_ =>
        {
            _deactivated.gameObject.SetActive(false);
            _activated.gameObject.SetActive(true);
        }).AddTo(this);
    }
}