using UnityEngine;
using System;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using Firebase.Auth;
using Firebase;
using Google;
using R3;

public class Authoring : IHaveAuthenticateEvent, IInitializable, IDisposable
{
    private FirebaseAuth _auth;
    private CancellationTokenSource _source;

    private Subject<string> _authenticated = new();

    public Observable<string> Authenticated => _authenticated;

    public void Initialize()
    {
        _source = new();

        Login(_source.Token).Forget();
    }

    public void Dispose()
    {
        _authenticated?.Dispose();

        _source?.Dispose();
        _auth?.Dispose();
    }

    private async UniTask Login(CancellationToken token)
    {
        await SetAuthoring(token);

        if (_source.IsCancellationRequested)
            return;

        SignIn(token).Forget();
    }

    private async UniTask SignIn(CancellationToken token)
    {
        if (_auth == null)
            return;

        try
        {
            //GoogleSignInUser user = await GoogleSignIn.DefaultInstance.SignIn().AsUniTask().AttachExternalCancellation(token);
            //Credential credential = GoogleAuthProvider.GetCredential(user.IdToken, null);

            //AuthResult result = await _auth.SignInAndRetrieveDataWithCredentialAsync(credential).AsUniTask().AttachExternalCancellation(token);

            _authenticated.OnNext("Vitaliis");
        }
        catch (Exception _)
        {
            Debug.LogError("Error");
        }
    }

    private async UniTask SetAuthoring(CancellationToken token)
    {
        try
        {
            DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask().AttachExternalCancellation(token);

            if (status == DependencyStatus.Available)
            {
                _auth = FirebaseAuth.DefaultInstance;

                GoogleSignIn.Configuration = new()
                {
                    WebClientId = "705900096007-6dkmma1r09ois2k4ca9rkton99bhcmnu.apps.googleusercontent.com",
                    RequestIdToken = true,
                    RequestEmail = true
                };
            }
            else
                Debug.LogError("Failed");
        }
        catch (Exception _)
        {
            Debug.LogError("Failed");
        }
    }
}
