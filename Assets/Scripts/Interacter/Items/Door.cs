using UnityEngine;

public class Door : Trigger, IInteractable
{
    public void Interact()
    {
        Debug.Log("A");
    }
}
