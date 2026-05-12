public class Key : Trigger, IInteractable
{
    public void Interact()
        => gameObject.SetActive(false);
}
