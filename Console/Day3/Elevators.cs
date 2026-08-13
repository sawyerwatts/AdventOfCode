using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AdventOfCode2025.Day3;

public class Elevators
{
    public Task<int> Challenge(Settings settings, CancellationToken ct = default)
    {
        var banks = ParseBanks(settings.InputPath, ct);
        return banks.Select(bank => bank.MaxJoltage(settings.BatteriesToEnable)).SumAsync(ct).AsTask();
    }

    private static async IAsyncEnumerable<BatteryBank> ParseBanks(string inputFile, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var lines = File.ReadLinesAsync(inputFile, ct);
        int lineNum = 0;
        await foreach (var line in lines)
        {
            lineNum++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            BatteryBank bank;
            try
            {
                bank = new BatteryBank(line);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Line {lineNum} encountered an error", e);
            }

            yield return bank;
        }
    }

    public class Settings
    {
        [Required] public string InputPath { get; set; } = "Day3/input.txt";
        [Range(2, int.MaxValue)] public int BatteriesToEnable { get; set; } = 2;
    }
}