using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AnimationPrioritiesData", menuName = "AnimationPriorities")]
public class AnimationPrioritiesData : ScriptableObject
{
    private Dictionary<AnimationNames, int> _animationPriority;

    private void OnEnable()
    {
        _animationPriority = new()
        {
            { AnimationNames.Idle, 1 },
            { AnimationNames.Walking, 2 },
            { AnimationNames.Climbing, 3 },
            { AnimationNames.Jumping, 4 }
        };
    }

    public bool IsMostPriority(AnimationNames first, AnimationNames second)
    {
        if (_animationPriority.TryGetValue(first, out int priority) == false)
            return false;

        return _animationPriority[second] < priority;
    }
}
