using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AdventOfCode2025.Day2;

// TODO: could run through checklists
// TODO: could write unit tests

public class GiftShop
{
    public async Task<long> Challenge(Settings settings, CancellationToken ct = default)
    {
        var ranges = ParseInput(settings, ct);

        long sum = 0;
        await foreach (var range in ranges)
        {
            for (long i = range.Lower; i <= range.Upper; i++)
            {
                if (IsRepeated(i, settings.Mode))
                    sum += i;
            }
        }

        return sum;
    }

    private static async IAsyncEnumerable<Range> ParseInput(Settings settings, [EnumeratorCancellation] CancellationToken ct)
    {
        var lines = File.ReadLinesAsync(settings.InputPath, ct);
        var lineNum = 0;
        await foreach (string line in lines)
        {
            lineNum++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Length > 0 && line[0].Equals('#'))
                continue;

            var rawRanges = line.Split(',');
            var rangeNum = 0;
            foreach (string rawRange in rawRanges)
            {
                rangeNum++;
                Range range;
                try
                {
                    var rangeParts = rawRange.Split('-');
                    if (rangeParts.Length != 2)
                        throw new InvalidOperationException("Encountered a range that does not have exactly two dash-delimited parts");

                    var lower = long.Parse(rangeParts[0]);
                    var upper = long.Parse(rangeParts[1]);
                    range = new Range(lower, upper);
                }
                catch (Exception exc)
                {
                    throw new InvalidOperationException($"Line {lineNum}, range number {rangeNum} ({rawRange}) has an issue", exc);
                }

                yield return range;
            }
        }
    }

    public static bool IsRepeated(long n, Mode mode)
    {
        var s = n.ToString();
        if (mode is Mode.PartIsRepeatedExactlyOnce)
        {
            if (s.Length % 2 != 0)
                return false;

            var left = s[..(s.Length / 2)];
            var right = s[(s.Length / 2)..];
            return left == right;
        }

        var len = s.Length;
        for (int segLen = 1; segLen <= (len / 2); segLen++)
        {
            if (len % segLen != 0)
                continue;

            var numSegs = len / segLen;
            var firstSeg = s[..segLen];

            var allMatching = true;
            for (int segNum = 1; segNum < numSegs; segNum++)
            {
                var segStart = segNum * segLen;
                var segEnd = segStart + segLen;
                var currSeg = s[segStart..segEnd];

                if (firstSeg != currSeg)
                {
                    allMatching = false;
                    break;
                }
            }

            if (allMatching)
                return true;
        }

        return false;
    }

    private readonly record struct Range
    {
        public long Lower { get; private init; }
        public long Upper { get; private init; }

        public Range(long lower, long upper)
        {
            if (lower >= upper)
                throw new InvalidOperationException($"The lower value ({lower}) of the range must be less than the upper value ({upper}) of the range");

            Lower = lower;
            Upper = upper;
        }

        public Range() : this(0, 0) => throw new NotSupportedException();

        public void Deconstruct(out long Lower, out long Upper)
        {
            Lower = this.Lower;
            Upper = this.Upper;
        }
    }

    public sealed class Settings
    {
        [Required] public string InputPath { get; set; } = "Day2/input.txt";
        public Mode Mode { get; set; } = Mode.PartIsRepeatedExactlyOnce;
    }

    public enum Mode
    {
        /// <summary>
        /// This is challenge 1 and the answer is 55916882972.
        /// </summary>
        PartIsRepeatedExactlyOnce,

        /// <summary>
        /// This is challenge 2 and the answer is 76169125915.
        /// </summary>
        PartIsRepeatedAtLeastOnce,
    }
}