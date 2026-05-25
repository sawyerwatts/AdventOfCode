using AdventOfCode2025.Day1;

Console.WriteLine(new MissingPassword().Challenge(new MissingPassword.Settings() { Mode = MissingPassword.Mode.CountZerosAfterMovement }));
Console.WriteLine(new MissingPassword().Challenge(new MissingPassword.Settings() { Mode = MissingPassword.Mode.CountZerosWheneverEncountered }));
