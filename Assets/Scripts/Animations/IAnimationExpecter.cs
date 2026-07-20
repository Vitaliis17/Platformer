using System.Collections.Generic;

public interface IAnimationExpecter
{
    void Add(AnimationNames name);

    void Remove(AnimationNames name);

    List<AnimationNames> GetNames();
}