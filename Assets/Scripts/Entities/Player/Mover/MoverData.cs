using UnityEngine;

[CreateAssetMenu(fileName = nameof(MoverData), menuName = "Movement/" + nameof(MoverData))]
public class MoverData : ScriptableObject
{
    [SerializeField, Min(0)] private float _speed;

    public float Speed => _speed;
}
