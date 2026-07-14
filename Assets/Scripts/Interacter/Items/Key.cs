using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public Vector2 Position => transform.position;

    public void Interact()
        => gameObject.SetActive(false);
}
