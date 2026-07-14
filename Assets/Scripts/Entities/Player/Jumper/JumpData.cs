using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = "Movement/Jump")]
public class JumpData : ScriptableObject
{
    [SerializeField] private Vector2 _force;

    public Vector2 Force => _force;
}
