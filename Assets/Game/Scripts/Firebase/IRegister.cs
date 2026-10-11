using Cysharp.Threading.Tasks;

public interface IRegister
{
    UniTask Register(string email, string password);
}