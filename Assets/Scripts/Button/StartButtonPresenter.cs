using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartButtonPresenter : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Inject] private INextLevelLoader _nextLevelLoader;

    private void Start()
        => _button.onClick.AddListener(_nextLevelLoader.LoadNextLevel);
}