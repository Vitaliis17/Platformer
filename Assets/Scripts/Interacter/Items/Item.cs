using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : Trigger, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
    }
}