using Cysharp.Threading.Tasks;
using System.Threading;

public interface ISignInService
{
    UniTask SignIn(CancellationToken token);
}