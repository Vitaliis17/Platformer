using UnityEngine;
using Zenject;

public class Transferator<T> : ITransferator<T>
{
    private readonly IPixelPerUnitSender _sender;

    [Inject]
    public Transferator(IPixelPerUnitSender sender)
        => _sender = sender;

    public void Transfer(Vector2 targetPosition, T interactable)
    {
        if (interactable == null)
            return;

        if (interactable is ITransferable transferable)
            transferable.Rigidbody.MovePosition(targetPosition);
    }

    public Vector2 TranslatePixelPosition(Vector2 pixelPosition)
        => Camera.main.ScreenToWorldPoint(pixelPosition);
}