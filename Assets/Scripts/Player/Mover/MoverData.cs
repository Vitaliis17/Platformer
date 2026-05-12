using UnityEngine;

[CreateAssetMenu(fileName = "MoverData", menuName = "Movement/Move")]
public class MoverData : ScriptableObject
{
    [SerializeField, Min(0)] private float _speed;

    public float Speed => _speed;
}
