using UnityEngine;
using Zenject;

public class Transferator : MonoBehaviour, ITransferator<IInteractable>
{
    [Inject] private ScreenData _screenData;

    public void Transfer(Vector2 delta, IInteractable interactable)
    {
        if (interactable == null)
            return;

        Vector2 deltaPosition = delta / _screenData.PixelPerUnit;

        if (interactable is MonoBehaviour moveableObject)
            moveableObject.transform.position += (Vector3)deltaPosition;
    }
}