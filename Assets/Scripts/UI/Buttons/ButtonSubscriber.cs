using UnityEngine;
using UnityEngine.UI;
using R3;

[RequireComponent(typeof(Button))]
public abstract class ButtonSubscriber : MonoBehaviour
{
    [SerializeField] private Button _button;

    protected Observable<Unit> Observable;

    private void Awake()
        => Observable = _button.OnClickAsObservable();
}