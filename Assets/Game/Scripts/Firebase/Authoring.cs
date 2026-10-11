using UnityEngine;
using Firebase.Auth;
using Firebase;
using System;
using Zenject;
using Cysharp.Threading.Tasks;
using R3;

public class Authoring : ICanLogin, IRegister, IHaveLoginEvent, IDisposable
{
    private FirebaseAuth _auth;
    private EmailChecker _checker;

    private Subject<Unit> _isLogin = new();

    public Observable<Unit> IsLogin => _isLogin;

    [Inject]
    public Authoring(EmailChecker checker)
    {
        _checker = checker;

        SetAuthoring().Forget();
    }

    public void Dispose()
    {
        _isLogin?.Dispose();
        _auth?.Dispose();
    }

    public async UniTask Login(string email, string password)
    {
        if (_auth == null)
            return;

        if (IsCorrectEmail(email) == false)
            return;

        try
        {
            await _auth.SignInWithEmailAndPasswordAsync(email, password);

            _isLogin.OnNext(Unit.Default);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public async UniTask Register(string email, string password)
    {
        if (_auth == null)
            return;

        if (IsCorrectEmail(email) == false)
            return;

        try
        {
            await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

            _isLogin.OnNext(Unit.Default);
        }
        catch (Exception _)
        {
            Debug.LogError("Error");
        }
    }

    private bool IsCorrectEmail(string email)
    {

        if (_checker.IsCorrectDomain(email) == false)
        {
            Debug.LogError("Используйте адрес с доменом @gmail.com");

            return false;
        }
        else if (_checker.IsCorrectSymbols(email) == false)
        {
            Debug.LogError("Email должен содержать один символ @");

            return false;
        }

        return true;
    }

    private async UniTask SetAuthoring()
    {
        try
        {
            DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (status == DependencyStatus.Available)
                _auth = FirebaseAuth.DefaultInstance;
            else
                Debug.LogError("Failed");
        }
        catch (Exception _)
        {
            Debug.LogError("Failed");
        }
    }
}
