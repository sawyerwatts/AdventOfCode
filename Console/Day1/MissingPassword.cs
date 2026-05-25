using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AdventOfCode2025.Day1;

// TODO: could move on or write unit tests for this class

public class MissingPassword
{
    public int Challenge(Settings? settings = null)
    {
        settings ??= new Settings();

        var counter = new CircularCounter(upperInclusive: 99, initial: 50);
        var movements = ReadInputMovements(settings.InputPath);

        int zeros = 0;

        // BUG: failing to reproduce old functionality w/ new code
        foreach (int movement in movements)
        {
            var increment = movement > 0 ? 1 : -1;
            for (int i = 0; i < Math.Abs(movement); i++)
            {
                // TODO: didn't decrement on movement=-11
                counter.Increment(increment);
                if (counter.Counter == 0 && settings.Mode is Mode.CountZerosWheneverEncountered)
                    zeros++;
            }

            if (counter.Counter == 0 && settings.Mode is Mode.CountZerosAfterMovement)
                zeros++;
        }

        return zeros;
    }

    private IEnumerable<int> ReadInputMovements(string settingsInputPath)
    {
        var lines = File.ReadLines(settingsInputPath);
        int lineNum = 0;
        foreach (string line in lines)
        {
            lineNum++;

            if (string.IsNullOrWhiteSpace(line))
                continue;

            int num;
            try
            {
                if (line.Length == 1)
                    throw new InvalidOperationException("The line is only a single character");

                var numPart = line[1..];
                var numAbs = int.Parse(numPart);
                if (numAbs == 0)
                    throw new InvalidOperationException("The number cannot be zero");

                num = line[0] switch
                {
                    'R' => numAbs,
                    'L' => -1 * numAbs,
                    _ => throw new InvalidOperationException("Unexpected leading character"),
                };
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"An error occurred on line {lineNum}. The syntax of the line should be '^[LR]\\d+$' (and the int cannot evaluate to 0). The line is: '{line}'", e);
            }

            yield return num;
        }
    }

    public enum Mode
    {
        /// <summary>
        /// This is challenge 1 and the answer is 1135.
        /// </summary>
        CountZerosAfterMovement,

        /// <summary>
        /// This is challenge 2 and the answer is 6558.
        /// </summary>
        CountZerosWheneverEncountered,
    }

    public sealed class Settings
    {
        [Required] public string InputPath { get; set; } = "Day1/input.txt";

        public Mode Mode { get; set; } = Mode.CountZerosAfterMovement;
    }
}