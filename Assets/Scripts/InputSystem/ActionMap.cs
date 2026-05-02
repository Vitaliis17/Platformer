using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ActionMap : MonoBehaviour, IActionMap
{
    protected InputActionMap Map;

    private void OnEnable()
        => Activate();

    private void OnDisable()
        => Deactivate();

    public void Activate()
        => Map?.Enable();

    public void Deactivate()
        => Map?.Disable();
}
