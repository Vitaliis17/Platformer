using UnityEngine;
using Zenject;

public class Transferator<T> : ITransferator<T>
{
    private readonly IPixelPerUnitSender _sender;

    [Inject]
    public Transferator(IPixelPerUnitSender sender)
        => _sender = sender;

    public void Transfer(Vector2 delta, T interactable)
    {
        if (interactable == null)
            return;

        if (interactable is MonoBehaviour moveableObject)
            moveableObject.transform.position += (Vector3)delta;
    }

    public Vector2 GetDeltaPosition(Vector2 delta)
        => delta / _sender.PixelPerUnit;
}