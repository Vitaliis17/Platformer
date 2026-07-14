using UnityEngine;

[CreateAssetMenu(fileName = nameof(JumpData), menuName = "Movement/" + nameof(JumpData))]
public class JumpData : ScriptableObject
{
    [SerializeField] private Vector2 _force;

    public Vector2 Force => _force;
}
