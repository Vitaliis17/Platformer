using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;

public class SignInButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Inject] private ISignInService _service;

    private void Start()
        => _button.OnClickAsObservable().Subscribe(_ => _service.SignIn(destroyCancellationToken)).AddTo(this);
}