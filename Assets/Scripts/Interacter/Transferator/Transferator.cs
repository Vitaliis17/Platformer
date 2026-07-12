using UnityEngine;
using Zenject;

public class Transferator : MonoBehaviour, ITransferator<IInteractable>
{
    [Inject] private ScreenData _screenData;

    public void Transfer(Vector2 delta, IInteractable interactable)
    {
        if (interactable == null)
            return;

        if (interactable is MonoBehaviour moveableObject)
            moveableObject.transform.position += (Vector3)delta;
    }

    public Vector2 GetDeltaPosition(Vector2 delta)
        => delta / _screenData.PixelPerUnit;
}