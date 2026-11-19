using UnityEngine;
using Zenject;
using R3;

public class PlayerFallerPresenter
{
    [SerializeField] private float _time;

    [Inject] private IInventoryContainer _container;
    [Inject] private IJumper _jumper;

    private void Start()
    {
    }
}
