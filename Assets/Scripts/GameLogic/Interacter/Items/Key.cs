public class Key : Item, IInteractable
{
    public void Interact()
        => gameObject.SetActive(false);
}
