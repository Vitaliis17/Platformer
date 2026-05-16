using UnityEngine;

[CreateAssetMenu(fileName = "LayerData", menuName = "LayerData")]
public class LayerData : ScriptableObject
{
    [SerializeField] private LayerMask _layer;

    public int Layer { get; private set; }

    private void OnValidate()
    {
        const int BinarySystemDivider = 2;

        Layer = (int)Mathf.Log(_layer, BinarySystemDivider);
    }
}
