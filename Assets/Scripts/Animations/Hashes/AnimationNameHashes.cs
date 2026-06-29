using System.Collections.Generic;
using UnityEngine;

public static class AnimationNameHashes
{
    private readonly static Dictionary<AnimationNames, int> _hashes = new()
    {
        { AnimationNames.Idle, Animator.StringToHash(nameof(AnimationNames.Idle)) },
        { AnimationNames.Walking, Animator.StringToHash(nameof(AnimationNames.Walking)) },
        { AnimationNames.Climbing, Animator.StringToHash(nameof(AnimationNames.Climbing)) },
        { AnimationNames.Jumping, Animator.StringToHash(nameof(AnimationNames.Jumping)) }
    };

    public static int GetHash(AnimationNames name)
    {
        if (_hashes.TryGetValue(name, out int value) == false)
            return 0;

        return value;
    }
}
