using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuButtonPresenter : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Inject] private IMenuLoader _menuLoader;

    private void Start()
        => _button.onClick.AddListener(_menuLoader.LoadMenu);
}