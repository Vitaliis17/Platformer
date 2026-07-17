using UnityEngine;

public class InventoryContainer : MonoBehaviour, IInventoryContainer
{
    [SerializeField] LayerData _layerData;

    private IHavePosition _current;

    private Transform _lastParent;
    private int _lastLayer;

    public void Set(IHavePosition interactable)
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

    public IHavePosition Get()
    {
        IHavePosition interactable = _current;

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