using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class Stick : MonoBehaviour
{
    [SerializeField] private OnScreenStick _stick;

    private void OnDisable()
    {
        _stick.enabled = false;
    }
}
