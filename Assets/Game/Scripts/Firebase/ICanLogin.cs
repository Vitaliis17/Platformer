using Cysharp.Threading.Tasks;

public interface ICanLogin
{
    UniTask Login(string email, string password);
}