using UnityEngine;

public class InventoryContainer : MonoBehaviour, IInventoryContainer
{
    [SerializeField] LayerData _layerData;

    private ITransferable _current;

    private Transform _lastParent;

    public void Set(ITransferable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;

        _lastParent = _current.Rigidbody.transform.parent;

        _current.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _current.Rigidbody.transform.SetParent(transform);
        _current.Rigidbody.transform.position = transform.position;
    }

    public ITransferable Get()
    {
        ITransferable interactable = _current;

        if (interactable is MonoBehaviour monoBehaviourObject)
        {
            monoBehaviourObject.transform.SetParent(_lastParent);
            _current.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        _current = null;
        _lastParent = null;

        return interactable;
    }

    public bool IsEmpty()
        => _current == null;
}