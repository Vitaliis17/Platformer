using UnityEngine;

public class PauseSwitcher : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minGameSpeed;
    [SerializeField, Min(0)] private float _baseGameSpeed;

    public void Pause()
        => Time.timeScale = _minGameSpeed;

    public void Unpause()
        => Time.timeScale = _baseGameSpeed;
}
