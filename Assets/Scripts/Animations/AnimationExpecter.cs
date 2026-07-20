using System.Collections.Generic;
using System.Linq;

public class AnimationExpecter : IAnimationExpecter
{
    private List<AnimationNames> _expectation;

    public AnimationExpecter()
        => _expectation = new();

    public void Add(AnimationNames name)
    {
        if (_expectation.Any(element => element == name))
            return;

        _expectation.Add(name);
    }

    public void Remove(AnimationNames name)
    {
        if (_expectation.Any(element => element == name) == false)
            return;

        _expectation.Remove(name);
    }

    public List<AnimationNames> GetNames()
        => new(_expectation);
}