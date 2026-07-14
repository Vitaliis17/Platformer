using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = nameof(AnimationPrioritiesData), menuName = nameof(AnimationPrioritiesData))]
public class AnimationPrioritiesData : ScriptableObject
{
    private Dictionary<AnimationNames, int> _animationPriority;

    private void OnEnable()
    {
        _animationPriority = new()
        {
            { AnimationNames.Idle, 1 },
            { AnimationNames.Walking, 2 },
            { AnimationNames.Jumping, 3 },
            { AnimationNames.Climbing, 4 }
        };
    }

    public bool IsMostPriority(AnimationNames newName, AnimationNames oldName)
    {
        if (_animationPriority.TryGetValue(newName, out int priority) == false)
            return false;

        return _animationPriority[oldName] < priority;
    }

    public AnimationNames IsMostPriority(List<AnimationNames> names)
    {
        AnimationNames mostPriorityName = AnimationNames.Idle;

        foreach(AnimationNames name in names)
        {
            if(IsMostPriority(name, mostPriorityName))
            {
                mostPriorityName = name;
            }
        }

        return mostPriorityName;
    }
}
