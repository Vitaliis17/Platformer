using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;

public class PauseGameActivator : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private Button _button;

    [Inject] private IPauser _pauser;

    private void Start()
    {
        Observable<Unit> buttonOnClick = _button.OnClickAsObservable();

        buttonOnClick.Subscribe(_ => _pauser.Pause()).AddTo(this);
        buttonOnClick.Subscribe(_ => _panel.gameObject.SetActive(true)).AddTo(this);
    }
}