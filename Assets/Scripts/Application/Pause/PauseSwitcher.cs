using UnityEngine;
using Zenject;

public class PauseSwitcher : IPauseSwitcher
{
    private readonly IGameSpeedSender _speedData;

    [Inject]
    public PauseSwitcher(IGameSpeedSender speedData)
        => _speedData = speedData;

    public void Pause()
        => Time.timeScale = _speedData.MinGameSpeed;

    public void Unpause()
        => Time.timeScale = _speedData.BaseGameSpeed;
}
