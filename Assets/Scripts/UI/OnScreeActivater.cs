using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.OnScreen;

public class OnScreenActivater : MonoBehaviour, IActivitySetter
{
    [SerializeField] private Image _image;
    [SerializeField] private OnScreenControl _control;

    public void Activate()
        => SetActive(true);

    public void Deactivate()
        => SetActive(false);

    private void SetActive(bool isActive)
    {
        _image.enabled = isActive;
        _control.enabled = isActive;
    }
}