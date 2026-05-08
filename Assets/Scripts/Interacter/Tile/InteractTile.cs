using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "InteractTile", menuName = "Tile/Interact")]
public class InteractTile : Tile, IInteractable
{
    public bool IsInteracting { get; private set; }

    public void Interact()
        => IsInteracting = false;

    public void StopInteraction()
        => IsInteracting = false;
}
