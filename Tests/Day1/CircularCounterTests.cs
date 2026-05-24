using AdventOfCode2025.Day1;

namespace AdventOfCode2025.Tests.Day1;

public class CircularCounterTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void TestConstructorHappy(int initial)
    {
        var sut = new CircularCounter(upperInclusive: 3, initial: initial);
        Assert.Equal(initial, sut.Counter);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public void TestConstructorSad(int initial)
    {
        Assert.Throws<ArgumentException>(() => new CircularCounter(upperInclusive: 3, initial: initial));
    }

    [Fact]
    public void TestIncrementNoRoll()
    {
        var sut = new CircularCounter(upperInclusive: 3);

        sut.Increment(1);
        Assert.Equal(1, sut.Counter);

        sut.Increment(1);
        Assert.Equal(2, sut.Counter);

        sut.Increment(-1);
        Assert.Equal(1, sut.Counter);

        sut.Increment(2);
        Assert.Equal(3, sut.Counter);
    }

    [Fact]
    public void TestIncrementRollOver()
    {
        var sut = new CircularCounter(upperInclusive: 3, initial: 3);
        Assert.Equal(3, sut.Counter);

        sut.Increment(1);
        Assert.Equal(0, sut.Counter);
    }

    [Fact]
    public void TestIncrementRollUnder()
    {
        var sut = new CircularCounter(upperInclusive: 3, initial: 0);
        Assert.Equal(0, sut.Counter);

        sut.Increment(-1);
        Assert.Equal(3, sut.Counter);
    }
}