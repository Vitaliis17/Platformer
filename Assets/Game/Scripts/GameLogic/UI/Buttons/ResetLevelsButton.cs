using UnityEngine;
using Zenject;
using UnityEngine.UI;
using R3;

public class ResetLevelsButton : MonoBehaviour
{
    [Inject] private ISaveReseter _saveReseter;

    [SerializeField] private Button _button;

    private void Start()
        => _button.OnClickAsObservable().Subscribe(_ => _saveReseter.ResetLevels()).AddTo(this);
}