using UnityEngine;

public class Transferator
{
    private IInteractable _interactable;

    public void Set(IInteractable item)
        => _interactable = item;

    public void SetEmpty()
        => _interactable = null;

    public bool IsEmpty()
        => _interactable == null;

    public void Transfer(Vector2 delta)
    {
        float pixelPerUnit = Camera.main.pixelHeight / (Camera.main.orthographicSize * 2f);
        Vector2 deltaPosition = delta / pixelPerUnit;

        ((MonoBehaviour)_interactable).transform.position += (Vector3)deltaPosition;
    }

    public IInteractable Get(int index)
    {
        throw new System.NotImplementedException();
    }
}
