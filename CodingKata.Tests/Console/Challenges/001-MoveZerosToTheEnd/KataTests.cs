using CodingKata.Console.Challenges._001_MoveZerosToTheEnd;

namespace CodingKata.Tests.Console.Challenges._001_MoveZerosToTheEnd;

public class KataTests
{
    [Fact]
    public void GivenTest()
    {
        int[] expected = [1, 2, 1, 1, 3, 1, 0, 0, 0, 0];
        int[] actual = Kata.MoveZeroes([1, 2, 0, 1, 0, 1, 0, 3, 0, 1]);
        Assert.Equal(expected.Length, actual.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            var currExpected = expected[i];
            var currActual = actual[i];
            Assert.Equal(currExpected, currActual);
        }
    }
}