using UnityEngine;
using UnityEngine.UI;

public class MenuButtonPresenter : MonoBehaviour
{
    [SerializeField] private Button _button;

    private LevelSwitcher _levelSwitcher;

    private void Awake()
    {
        _levelSwitcher = FindFirstObjectByType<LevelSwitcher>();

        if (_levelSwitcher != null)
            _button.onClick.AddListener(_levelSwitcher.LoadMenu);
    }
}