using Cysharp.Threading.Tasks;

public interface IRevisionReaderAsync
{
    UniTask<int> ReadRevision();
}