using UnityEngine;

public class InventoryContainer : MonoBehaviour, IInventoryContainer
{
    [SerializeField] LayerData _layerData;

    private IInteractable _current;

    private Transform _lastParent;
    private int _lastLayer;

    public void Set(IInteractable interactable)
    {
        if (_current != null)
            return;

        _current = interactable;

        if (_current is MonoBehaviour moveableObject)
        {
            _lastParent = moveableObject.transform.parent;
            _lastLayer = moveableObject.gameObject.layer;

            moveableObject.transform.SetParent(transform);

            moveableObject.transform.localPosition = Vector3.zero;
            moveableObject.gameObject.layer = _layerData.Layer;
        }
    }

    public IInteractable Get()
    {
        IInteractable interactable = _current;

        if(interactable is MonoBehaviour monoBehaviourObject)
        {
            monoBehaviourObject.transform.SetParent(_lastParent);
            monoBehaviourObject.gameObject.layer = _lastLayer;
        }

        _current = null;
        _lastParent = null;

        return interactable;
    }

    public bool IsEmpty()
        => _current == null;
}