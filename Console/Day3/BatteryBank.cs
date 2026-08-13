using System.Runtime.InteropServices;

namespace AdventOfCode2025.Day3;

public class BatteryBank
{
    /// <summary>
    /// Each battery is a 1-9 joltage value.
    /// </summary>
    public IReadOnlyList<int> Batteries { get; }

    public BatteryBank(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException($"{nameof(input)} is null or whitespace, but a bank must have at least two batteries");

        input = input.Trim();
        var batteries = new List<int>(capacity: input.Length);
        int charNum = -1;
        foreach (var c in input)
        {
            charNum++;
            if (!char.IsDigit(c))
                throw new ArgumentException($"Character number {charNum} (zero-indexed) is not a digit: '{c}'");

            var joltage = (int)char.GetNumericValue(c);
            if (joltage is < 1 or > 9)
                throw new ArgumentException($"Character number {charNum} (zero-indexed) must be 1-9 but is '{joltage}'");

            batteries.Add(joltage);
        }

        Batteries = batteries.AsReadOnly();
    }

    public int MaxJoltage(int numBatteriesToEnable)
    {
        if (Batteries.Count < numBatteriesToEnable)
            throw new ArgumentException($"Cannot enable {numBatteriesToEnable} batteries, there are only {Batteries.Count} in the bank");

        // TODO: Redo this alg w/ a variable number of batteries
        // O(n): find highest number and the highest number of left/right of that number, then use highest combo of highest+left or highest+right
        var left = -1;
        var highest = -1;
        var right = -1;
        foreach (var joltage in Batteries)
        {
            if (joltage >= highest)
            {
                left = highest;
                highest = joltage;
                right = -1;
            }
            else if (joltage > right)
            {
                right = joltage;
            }
        }

        var leftJoltage = Combine(left, highest);
        var rightJoltage = Combine(highest, right);
        return Math.Max(leftJoltage, rightJoltage);

        static int Combine(int left, int right) => left == -1 || right == -1
            ? 0
            : (left * 10) + right;
    }

    public int MaxJoltageAlt(int numBatteriesToEnable)
    {
        if (Batteries.Count < numBatteriesToEnable)
            throw new ArgumentException($"Cannot enable {numBatteriesToEnable} batteries, there are only {Batteries.Count} in the bank");

        // TODO: this
        // 1. a version of divide and conquer: O(n*log(n))
        //      find highest digit
        //          if on far right, ignore
        //          if one, continue
        //          if multiple, recurse each and take the larger
        //      recursive helper to get remaining digits
        //      combine

        throw new NotImplementedException();
    }

    public int MaxJoltageBroken(int numBatteriesToEnable)
    {
        if (Batteries.Count < numBatteriesToEnable)
            throw new ArgumentException($"Cannot enable {numBatteriesToEnable} batteries, there are only {Batteries.Count} in the bank");

        // 2. single pass (through bank, but backtracking on result): O(n*p)
        //      foreach joltage
        //          travel backwards on result, bounded by remaining joltages
        //              if result[j] is greater than joltage, break result
        //              if result[j] is lower than joltage
        //                  store j as lowest index
        //          if there's a j
        //              replace result[j]
        //              clear result[j+1..]
        //              continue joltage
        //          if there's an empty slot at end, throw in there and continue
        // NOTE: this path is death, the alg doesn't work. Consider 13829.
        //      First digit: 1->3->8
        //      Second digit: 2->9
        //      No third digit. Would have to constantly forward scan to confirm this works.

        var result = new List<int>(capacity: numBatteriesToEnable);
        for (var i = 0; i < result.Capacity; i++)
            result.Add(0);
        var resultCount = 0;

        for (var i = 0; i < Batteries.Count; i++)
        {
            var joltage = Batteries[i];
            int? lowestEarliestJ = null;
            for (int j = resultCount-1, backset = 0; j >= 0 && i+backset < Batteries.Count; j--, backset++)
            {
                var check = result[j];
                if (check > joltage)
                    break;
                if (check == joltage)
                    continue;
                lowestEarliestJ = j;
            }

            // BUG: this code is greedy, like a regex, and it replaces too much
            if (lowestEarliestJ is not null)
            {
                result[lowestEarliestJ.Value] = joltage;
                resultCount = lowestEarliestJ.Value + 1;
            } else if (resultCount < numBatteriesToEnable)
            {
                result[resultCount] = joltage;
                resultCount++;
            }
        }

        if (resultCount != result.Count)
            throw new InvalidOperationException($"{nameof(resultCount)} is {resultCount} while {nameof(result)}.{nameof(result.Count)} is {result.Count}");

        var r = 0;
        for (var i = 0; i < result.Count; i++)
        {
            var exponent = result.Count - i - 1;
            var tens = (int)Math.Pow(10, exponent);
            var curr = result[i] * tens;
            r += curr;
        }

        return r;
    }

    public override string ToString() => string.Join("", Batteries);
}