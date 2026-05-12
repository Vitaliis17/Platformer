using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ActionMap : MonoBehaviour, IActionMap
{
    protected InputActionMap Map;

    protected virtual void OnEnable()
        => Activate();

    protected virtual void OnDisable()
        => Deactivate();

    public void Activate()
    {
        Debug.Log(Map);
        Map?.Enable();
    }

    public void Deactivate()
        => Map?.Disable();
}
