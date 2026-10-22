using Cysharp.Threading.Tasks;
using System.Threading;

public interface ISaveReaderAsync
{
    UniTask<SaveData> Read(CancellationToken token);
}