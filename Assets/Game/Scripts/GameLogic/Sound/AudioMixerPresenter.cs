using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;

public class AudioMixerPresenter : MonoBehaviour
{
    [Inject] private IMuter _muter;

    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.isOn = _muter.IsMute() == false;

        Observable<bool> observable = _toggle.OnValueChangedAsObservable();

        observable.Where(isActive => isActive).Subscribe(_ => _muter.Unmute()).AddTo(this);
        observable.Where(isActive => isActive == false).Subscribe(_ => _muter.Mute()).AddTo(this);
    }
}