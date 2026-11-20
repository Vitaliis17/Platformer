using UnityEngine;
using System.Collections.Generic;

public static class AnimationParameterHashes
{
    private readonly static Dictionary<AnimationNames, int> _hashes = new()
    {
        { AnimationNames.Idle, Animator.StringToHash(nameof(AnimationParameterNames.IsIdle)) },
        { AnimationNames.Walking, Animator.StringToHash(nameof(AnimationParameterNames.IsWalking)) },
        { AnimationNames.Climbing, Animator.StringToHash(nameof(AnimationParameterNames.IsClimbing)) },
        { AnimationNames.Jumping, Animator.StringToHash(nameof(AnimationParameterNames.IsJumping)) },
        { AnimationNames.Falling, Animator.StringToHash(nameof(AnimationParameterNames.IsFalling)) },
        { AnimationNames.Landing, Animator.StringToHash(nameof(AnimationParameterNames.IsLanding)) }
    };

    public static int GetHash(AnimationNames name)
    {
        if (_hashes.TryGetValue(name, out int value) == false)
            return 0;

        return value;
    }
}
