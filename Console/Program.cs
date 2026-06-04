using AdventOfCode2025.Day1;
using AdventOfCode2025.Day2;

// Console.WriteLine(new MissingPassword().Challenge(new MissingPassword.Settings() { Mode = MissingPassword.Mode.CountZerosAfterMovement }));
// Console.WriteLine(new MissingPassword().Challenge(new MissingPassword.Settings() { Mode = MissingPassword.Mode.CountZerosWheneverEncountered }));
Console.WriteLine(await new GiftShop().Challenge(new GiftShop.Settings() { Mode = GiftShop.Mode.PartIsRepeatedExactlyOnce }));
Console.WriteLine(await new GiftShop().Challenge(new GiftShop.Settings() { Mode = GiftShop.Mode.PartIsRepeatedAtLeastOnce }));
