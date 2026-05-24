using System;

namespace AdventOfCode2025.Day1;

public class MissingPassword
{
    public static int ChallengeOne()
    {
        var counter = new CircularCounter(upperInclusive: 99, initial: 50);

        // TODO: read instructions and apply

        // TODO: return the number of times the dial lands on 0

        throw new NotImplementedException();
    }
}

/// <summary>
/// Circular counter from zero to a specified upper bound.
/// </summary>
public class CircularCounter
{
    private readonly int _upperInclusive;

    public int Counter
    {
        get;
        private set
        {
            if (value < 0)
                throw new ArgumentException($"Cannot set {nameof(CircularCounter.Counter)} to {value} because the counter should be zero or positive");
            if (value > _upperInclusive)
                throw new ArgumentException($"Cannot set {nameof(CircularCounter.Counter)} to {value} because it is higher than the upper bound (inclusive) of {_upperInclusive}");
            field = value;
        }
    }

    public CircularCounter(int upperInclusive, int initial = 0)
    {
        _upperInclusive = upperInclusive;
        Counter = initial;
    }

    public void Increment(int n)
    {
        var newCounter = Counter + n;
        while (newCounter > _upperInclusive)
        {
            newCounter--;
            newCounter -= _upperInclusive;
        }

        while (newCounter < 0)
        {
            newCounter++;
            newCounter += _upperInclusive;
        }

        Counter = newCounter;
    }
}