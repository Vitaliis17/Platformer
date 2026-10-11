using R3;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginView : MonoBehaviour, IHavePressedEvent
{
	[SerializeField] private TMP_InputField _emailField;
	[SerializeField] private TMP_InputField _passwordField;

	[SerializeField] private Button _loginButton;

	public Observable<(string, string)> Pressed { get; private set; }

    private void Awake()
    {
        Pressed = _loginButton
            .OnClickAsObservable()
            .Select(_ => (_emailField.text, _passwordField.text));
    }
}