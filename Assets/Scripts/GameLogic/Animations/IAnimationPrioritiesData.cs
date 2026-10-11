using System.Collections.Generic;

public interface IAnimationPrioritiesData
{
    bool IsMostPriority(AnimationNames newName, AnimationNames oldName);

    AnimationNames IsMostPriority(List<AnimationNames> names);

}