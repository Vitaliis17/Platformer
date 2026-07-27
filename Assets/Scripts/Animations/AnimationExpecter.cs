using System.Collections.Generic;

public class AnimationExpecter : IAnimationExpecter
{
    private List<AnimationNames> _expectation;
    private HashSet<AnimationNames> _uniqueExpectation;

    public AnimationExpecter()
    {
        _expectation = new();
        _uniqueExpectation = new();
    }

    public void Add(AnimationNames name)
    {
        if (_uniqueExpectation.Add(name) == false)
            return;

        _expectation.Add(name);
    }

    public void Remove(AnimationNames name)
    {
        if (_uniqueExpectation.Remove(name) == false)
            return;

        _expectation.Remove(name);
    }

    public List<AnimationNames> GetNames()
        => new(_expectation);
}