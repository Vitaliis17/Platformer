using R3;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegisterView : MonoBehaviour, IHavePressedEvent
{
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_InputField _confirmationPasswordField;

    [SerializeField] private Button _registerButton;

    public Observable<(string, string)> Pressed { get; private set; }

    private void Awake()
    {
        Pressed = _registerButton
            .OnClickAsObservable()
            .Select(_ => (_emailField.text, _passwordField.text))
            .Where(info => IsEqualPasswords(info.Item2, _confirmationPasswordField.text));
    }

    private bool IsEqualPasswords(string password, string confirmationPassword)
        => password == confirmationPassword;
}