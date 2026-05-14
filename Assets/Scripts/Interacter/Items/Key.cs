using UnityEngine;

public class Key : Trigger, IInteractable
{
    public Vector2 Position => transform.position;

    public void Interact()
        => gameObject.SetActive(false);
}
