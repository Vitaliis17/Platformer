using UnityEngine;
using Zenject;

public class PauseSwitcher : MonoBehaviour, IPauseSwitcher
{
    [Inject] private PauseData _data;

    public void Pause()
        => Time.timeScale = _data.MinGameSpeed;

    public void Unpause()
        => Time.timeScale = _data.BaseGameSpeed;

    private void OnDestroy()
        => Unpause();
}
