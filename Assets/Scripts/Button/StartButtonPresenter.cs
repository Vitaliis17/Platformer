using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartButtonPresenter : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueButton;

    [Inject] private INextLevelLoader _nextLevelLoader;

    private void Start()
    {
        _newGameButton.onClick.AddListener(_nextLevelLoader.LoadNextLevel);
        _continueButton.onClick.AddListener(_nextLevelLoader.LoadNextLevel);
    }
}