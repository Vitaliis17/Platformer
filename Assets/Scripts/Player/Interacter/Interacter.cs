using UnityEngine;

public class Interacter : MonoBehaviour, IInteracter
{
    public void Interact(IInteractable interactable)
    {
        if (interactable == null)
            return;

        interactable.Interact();
    }
}
