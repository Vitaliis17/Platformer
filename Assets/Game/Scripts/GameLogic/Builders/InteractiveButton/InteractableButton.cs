using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using R3;

[RequireComponent(typeof(Collider2D))]
public class InteractiveButton : MonoBehaviour
{
    [Inject(Id = IdNames.Button)] private IHaveActivityWithEvent _activiter;

    private int _collisionAmount = 0;

    private void Start()
        => _activiter.Activated.Subscribe(_ => SetDeactivation().Forget()).AddTo(this);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collisionAmount == 0)
            _activiter.Activate();

        _collisionAmount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
        => _collisionAmount--;

    private async UniTask SetDeactivation()
    {
        await UniTask.WaitUntil(() => _collisionAmount == 0, cancellationToken: this.GetCancellationTokenOnDestroy());

        _activiter.Deactivate();
    }
}