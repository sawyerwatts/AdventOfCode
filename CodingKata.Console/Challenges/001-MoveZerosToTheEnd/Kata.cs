namespace CodingKata.Console.Challenges._001_MoveZerosToTheEnd;

/// <summary>
/// Write an algorithm that takes an array and moves all of the zeros to the end, preserving the order of the other elements.
/// <example>
/// Kata.MoveZeroes(new int[] {1, 2, 0, 1, 0, 1, 0, 3, 0, 1}) => new int[] {1, 2, 1, 1, 3, 1, 0, 0, 0, 0}
/// </example>
/// </summary>
/// <remarks>
/// 5 Kyu
/// </remarks>
public class Kata
{
    /// <inheritdoc cref="Kata"/>
    public static int[] MoveZeroes(int[] arr)
    {
        return Option2(arr);
    }

    private static int[] Option1(int[] arr)
    {
        // option 1
        //      count zeros
        //      "compress" array's non-zero values
        //      pad array with zeros
        var numZeros = arr.Count(n => n == 0);
        var zeros = Enumerable.Range(0, numZeros).Select(_ => 0);
        var nonZeros = arr.Where(n => n != 0);
        var moved = nonZeros.Concat(zeros);
        return moved.ToArray();
    }

    private static int[] Option2(int[] arr)
    {
        // option 2
        //      make a new array w/ same length and all zeros
        //      loop over first list
        //          if non-zero, put in last non-zero index

        var newArr = new int[arr.Length];
        var iNewArr = 0;

        foreach (var n in arr)
        {
            if (n == 0)
                continue;

            newArr[iNewArr] = n;
            iNewArr++;
        }

        return newArr;
    }
}