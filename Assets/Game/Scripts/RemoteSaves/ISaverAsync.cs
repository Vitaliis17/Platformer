using Cysharp.Threading.Tasks;
using System.Threading;

public interface ISaverAsync
{
    public UniTask Save(int levelNumber, long revision, CancellationToken token);
}